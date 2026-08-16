using LegendsTeamVN.BadmintonClub.Application.DTOs.VenueSchedules.Responses;
using LegendsTeamVN.BadmintonClub.Domain.Repositories;
using LegendsTeamVN.Core.Application.Messaging.CQRS;
using LegendsTeamVN.Core.Utilities.Results;

namespace LegendsTeamVN.BadmintonClub.Application.Features.VenueSchedules.GetById;

public sealed class GetVenueScheduleByIdQueryHandler(
    IVenueScheduleRepository venueScheduleRepository
) : IQueryHandler<GetVenueScheduleByIdQuery, VenueScheduleResponse>
{
    public async Task<Result<VenueScheduleResponse>> Handle(GetVenueScheduleByIdQuery request, CancellationToken cancellationToken)
    {
        var schedule = await venueScheduleRepository.FindByIdAsync(request.Id, cancellationToken);

        if (schedule is null)
        {
            return Result.Failure<VenueScheduleResponse>(Error.NotFound("VenueSchedule.NotFound", "The venue schedule with the specified ID was not found."));
        }

        var response = new VenueScheduleResponse(
            schedule.Id,
            schedule.VenueId,
            schedule.DayOfWeek,
            schedule.OpenTime,
            schedule.CloseTime,
            schedule.IsClosed,
            schedule.CreatedOnUtc,
            schedule.ModifiedOnUtc
        );

        return Result.Success(response);
    }
}
