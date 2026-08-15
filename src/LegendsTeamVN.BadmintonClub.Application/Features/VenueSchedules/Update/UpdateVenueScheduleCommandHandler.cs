using LegendsTeamVN.BadmintonClub.Domain.Repositories;
using LegendsTeamVN.Core.Application.Messaging.CQRS;
using LegendsTeamVN.Core.Utilities.Results;

namespace LegendsTeamVN.BadmintonClub.Application.Features.VenueSchedules.Update;

public sealed class UpdateVenueScheduleCommandHandler(
    IVenueScheduleRepository venueScheduleRepository
) : ICommandHandler<UpdateVenueScheduleCommand>
{
    public async Task<Result> Handle(UpdateVenueScheduleCommand request, CancellationToken cancellationToken)
    {
        var schedule = await venueScheduleRepository.FindByIdAsync(request.Id, cancellationToken);

        if (schedule is null)
        {
            return Result.Failure(Error.NotFound("VenueSchedule.NotFound", "The venue schedule with the specified ID was not found."));
        }

        schedule.UpdateSchedule(request.OpenTime, request.CloseTime, request.IsClosed);

        venueScheduleRepository.Update(schedule);

        return Result.Success();
    }
}
