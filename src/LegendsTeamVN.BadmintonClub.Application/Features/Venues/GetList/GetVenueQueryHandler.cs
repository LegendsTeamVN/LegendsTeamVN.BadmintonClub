using LegendsTeamVN.BadmintonClub.Domain.Entities;
using LegendsTeamVN.BadmintonClub.Application.DTOs.Venues.Responses;
using LegendsTeamVN.BadmintonClub.Domain.Repositories;
using LegendsTeamVN.Core.Application.Messaging.CQRS;
using LegendsTeamVN.Core.Utilities.Pagination;
using LegendsTeamVN.Core.Utilities.Results;
using LegendsTeamVN.Core.Application.Messaging.CQRS;
using LegendsTeamVN.Core.Utilities.Results;

namespace LegendsTeamVN.BadmintonClub.Application.Features.Venues.GetList;

public sealed class GetVenueQueryHandler(IVenueRepository venueRepository): IQueryHandler<GetVenueQuery, PagedResult<VenueResponse>>{
    public async Task<Result<PagedResult<VenueResponse>>> Handle(GetVenueQuery request , CancellationToken cancellationToken ){
        var filter = request.Filter;
        var query = venueRepository.FindAll();
        
        if (!string.IsNullOrWhiteSpace(filter.Name))
            query = query.Where(x => x.Name.Contains(filter.Name));
        if (!string.IsNullOrWhiteSpace(filter.Address))
            query = query.Where(x => x.Address.Contains(filter.Address));
        if (filter.OpenTime.HasValue) 
            query = query.Where(x => x.OpenTime == filter.OpenTime.Value);
        if (filter.CloseTime.HasValue) 
            query = query.Where(x => x.CloseTime == filter.CloseTime.Value);
        if (filter.Latitude.HasValue)
            query = query.Where(x => x.Latitude == filter.Latitude.Value);
        if (filter.Longitude.HasValue)
            query = query.Where(x => x.Longitude == filter.Longitude.Value);
        
        var pagedResult = await query
            .Select(venue => new VenueResponse(
                venue.Id,
                venue.Name,
                venue.Description,
                venue.Address,
                venue.Latitude,
                venue.Longitude,
                venue.OpenTime,
                venue.CloseTime,
                venue.IsActive
            ))
            .ToPagedResultAsync(filter.PageNumber, filter.PageSize, cancellationToken);
        return Result.Success(pagedResult);
    }
}