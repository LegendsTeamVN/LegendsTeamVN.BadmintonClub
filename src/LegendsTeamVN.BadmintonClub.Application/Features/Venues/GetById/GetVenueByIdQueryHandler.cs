using LegendsTeamVN.BadmintonClub.Application.DTOs.Venues.Responses;
using LegendsTeamVN.BadmintonClub.Domain.Entities;
using LegendsTeamVN.BadmintonClub.Domain.Repositories;
using LegendsTeamVN.Core.Application.Messaging.CQRS;
using LegendsTeamVN.Core.Utilities.Results;
    
namespace LegendsTeamVN.BadmintonClub.Application.Features.Venues.GetById;

public sealed class GetVenueByIdQueryHandler(IVenueRepository venueRepository): IQueryHandler<GetVenueByIdQuery, VenueResponse>{
    public async Task<Result<VenueResponse>> Handle(GetVenueByIdQuery request , CancellationToken cancellationToken ){
        var venue = await venueRepository.FindByIdAsync(request.Id,cancellationToken);
        if(venue is null){
            return Result.Failure<VenueResponse>(Error.NotFound("Venue.NotFound","A venue with this id does not exist."));
        }
        return Result.Success(new VenueResponse(
            venue.Id,
            venue.Name,
            venue.Description,
            venue.Address,
            venue.Latitude,
            venue.Longitude,
            venue.OpenTime,
            venue.CloseTime,
            venue.IsActive
        ));
    }
}