using LegendsTeamVN.BadmintonClub.Application.DTOs.CourtPricings.Responses;
using LegendsTeamVN.BadmintonClub.Domain.Repositories;
using LegendsTeamVN.Core.Application.Messaging.CQRS;
using LegendsTeamVN.Core.Utilities.Pagination;
using LegendsTeamVN.Core.Utilities.Results;

namespace LegendsTeamVN.BadmintonClub.Application.Features.CourtPricings.GetList;

public sealed class GetCourtPricingsQueryHandler(
    ICourtPricingRepository courtPricingRepository
) : IQueryHandler<GetCourtPricingsQuery, PagedResult<CourtPricingResponse>>
{
    public async Task<Result<PagedResult<CourtPricingResponse>>> Handle(GetCourtPricingsQuery request, CancellationToken cancellationToken)
    {
        var filter = request.Filter;
        var query = courtPricingRepository.FindAll();

        query = query.ApplyBaseFilter(filter);

        if (filter.VenueId.HasValue)
        {
            query = query.Where(x => x.VenueId == filter.VenueId.Value);
        }

        if (filter.DayOfWeek.HasValue)
        {
            query = query.Where(x => x.DayOfWeek == filter.DayOfWeek.Value);
        }

        if (filter.IsPeakHour.HasValue)
        {
            query = query.Where(x => x.IsPeakHour == filter.IsPeakHour.Value);
        }

        var pagedResult = await query
            .Select(p => new CourtPricingResponse(
                p.Id,
                p.VenueId,
                p.StartTime,
                p.EndTime,
                p.PricePerHour,
                p.DayOfWeek,
                p.IsPeakHour,
                p.CreatedOnUtc,
                p.ModifiedOnUtc
            ))
            .ToPagedResultAsync(filter.PageNumber, filter.PageSize, cancellationToken);

        return Result.Success(pagedResult);
    }
}
