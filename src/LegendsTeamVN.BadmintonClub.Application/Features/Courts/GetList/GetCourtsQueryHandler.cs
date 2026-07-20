using LegendsTeamVN.BadmintonClub.Application.DTOs.Courts.Responses;
using LegendsTeamVN.BadmintonClub.Domain.Repositories;
using LegendsTeamVN.Core.Application.Messaging.CQRS;
using LegendsTeamVN.Core.Utilities.Pagination;
using LegendsTeamVN.Core.Utilities.Results;

namespace LegendsTeamVN.BadmintonClub.Application.Features.Courts.GetList;

public sealed class GetCourtsQueryHandler(ICourtRepository courtRepository) : IQueryHandler<GetCourtsQuery, PagedResult<CourtResponse>>
{
    public async Task<Result<PagedResult<CourtResponse>>> Handle(GetCourtsQuery request, CancellationToken cancellationToken)
    {
        var filter = request.Filter;
        var query = courtRepository.FindAll();

        query = query.ApplyBaseFilter(filter, "Name");

        if (filter.IsAvailable.HasValue)
        {
            query = query.Where(x => x.IsAvailable == filter.IsAvailable.Value);
        }

        if (filter.MinPrice.HasValue)
        {
            query = query.Where(x => x.PricePerHour >= filter.MinPrice.Value);
        }

        if (filter.MaxPrice.HasValue)
        {
            query = query.Where(x => x.PricePerHour <= filter.MaxPrice.Value);
        }

        var pagedResult = await query
            .Select(court => new CourtResponse(court.Id, court.Name, court.Description, court.PricePerHour, court.IsAvailable))
            .ToPagedResultAsync(filter.PageNumber, filter.PageSize, cancellationToken);

        return Result.Success(pagedResult);
    }
}
