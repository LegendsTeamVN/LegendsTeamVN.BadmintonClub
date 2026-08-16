using LegendsTeamVN.Core.Application.Messaging.CQRS;

namespace LegendsTeamVN.BadmintonClub.Application.Features.VenueSchedules.Update;

public sealed record UpdateVenueScheduleCommand(
    Guid Id,
    TimeOnly OpenTime,
    TimeOnly CloseTime,
    bool IsClosed
) : ICommand;
