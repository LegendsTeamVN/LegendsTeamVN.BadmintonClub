using LegendsTeamVN.Core.Application.Messaging.CQRS;

namespace LegendsTeamVN.BadmintonClub.Application.Features.VenueSchedules.Create;

public sealed record CreateVenueScheduleCommand(
    Guid VenueId,
    int DayOfWeek,
    TimeOnly OpenTime,
    TimeOnly CloseTime,
    bool IsClosed
) : ICommand<Guid>;
