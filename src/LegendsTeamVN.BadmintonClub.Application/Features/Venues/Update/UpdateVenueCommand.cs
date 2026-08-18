using LegendsTeamVN.Core.Application.Messaging.CQRS;

namespace LegendsTeamVN.BadmintonClub.Application.Features.Venues.Update;

public sealed record UpdateVenueCommand(
    Guid Id,
    string Name,
    string? Description,
    string Address,
    decimal Latitude,
    decimal Longitude,
    TimeOnly OpenTime,
    TimeOnly CloseTime
) : ICommand;