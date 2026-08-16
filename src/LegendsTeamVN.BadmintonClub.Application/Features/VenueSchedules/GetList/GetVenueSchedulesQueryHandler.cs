using LegendsTeamVN.BadmintonClub.Application.DTOs.VenueSchedules.Responses;
using LegendsTeamVN.BadmintonClub.Domain.Repositories;
using LegendsTeamVN.Core.Application.Messaging.CQRS;
using LegendsTeamVN.Core.Utilities.Pagination;
using LegendsTeamVN.Core.Utilities.Results;

namespace LegendsTeamVN.BadmintonClub.Application.Features.VenueSchedules.GetList;

public sealed class GetVenueSchedulesQueryHandler(
    IVenueScheduleRepository venueScheduleRepository
) : IQueryHandler<GetVenueSchedulesQuery, PagedResult<VenueScheduleResponse>>
{
    public async Task<Result<PagedResult<VenueScheduleResponse>>> Handle(GetVenueSchedulesQuery request, CancellationToken cancellationToken)
    {
        var filter = request.Filter;
        var query = venueScheduleRepository.FindAll();

        query = query.ApplyBaseFilter(filter);

        if (filter.VenueId.HasValue)
        {
            query = query.Where(x => x.VenueId == filter.VenueId.Value);
        }

        if (filter.DayOfWeek.HasValue)
        {
            query = query.Where(x => x.DayOfWeek == filter.DayOfWeek.Value);
        }

        if (filter.IsClosed.HasValue)
        {
            query = query.Where(x => x.IsClosed == filter.IsClosed.Value);
        }

        var pagedResult = await query
            .Select(s => new VenueScheduleResponse(
                s.Id,
                s.VenueId,
                s.DayOfWeek,
                s.OpenTime,
                s.CloseTime,
                s.IsClosed,
                s.CreatedOnUtc,
                s.ModifiedOnUtc
            ))
            .ToPagedResultAsync(filter.PageNumber, filter.PageSize, cancellationToken);

        return Result.Success(pagedResult);
    }
}
