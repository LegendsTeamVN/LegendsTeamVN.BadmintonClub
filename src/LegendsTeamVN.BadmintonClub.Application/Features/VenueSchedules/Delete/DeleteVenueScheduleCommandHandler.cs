using LegendsTeamVN.BadmintonClub.Domain.Repositories;
using LegendsTeamVN.Core.Application.Messaging.CQRS;
using LegendsTeamVN.Core.Utilities.Results;

namespace LegendsTeamVN.BadmintonClub.Application.Features.VenueSchedules.Delete;

public sealed class DeleteVenueScheduleCommandHandler(
    IVenueScheduleRepository venueScheduleRepository
) : ICommandHandler<DeleteVenueScheduleCommand>
{
    public async Task<Result> Handle(DeleteVenueScheduleCommand request, CancellationToken cancellationToken)
    {
        var schedule = await venueScheduleRepository.FindByIdAsync(request.Id, cancellationToken);

        if (schedule is null)
        {
            return Result.Failure(Error.NotFound("VenueSchedule.NotFound", "The venue schedule with the specified ID was not found."));
        }

        venueScheduleRepository.Remove(schedule);

        return Result.Success();
    }
}
