using LegendsTeamVN.BadmintonClub.Domain.Entities;
using LegendsTeamVN.BadmintonClub.Domain.Repositories;
using LegendsTeamVN.Core.Application.Messaging.CQRS;
using LegendsTeamVN.Core.Utilities.Results;

namespace LegendsTeamVN.BadmintonClub.Application.Features.VenueSchedules.Create;

public sealed class CreateVenueScheduleCommandHandler(
    IVenueScheduleRepository venueScheduleRepository
) : ICommandHandler<CreateVenueScheduleCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateVenueScheduleCommand request, CancellationToken cancellationToken)
    {
        var existingSchedule = await venueScheduleRepository.FindSingleAsync(
            s => s.VenueId == request.VenueId && s.DayOfWeek == request.DayOfWeek,
            cancellationToken
        );

        if (existingSchedule is not null)
        {
            return Result.Failure<Guid>(Error.Conflict("VenueSchedule.AlreadyExists", "A schedule for this day of week already exists for this venue."));
        }

        var schedule = new VenueSchedule(
            request.VenueId,
            request.DayOfWeek,
            request.OpenTime,
            request.CloseTime,
            request.IsClosed
        );

        venueScheduleRepository.Add(schedule);

        return schedule.Id;
    }
}
