using LegendsTeamVN.Core.Application.Messaging.CQRS;

namespace LegendsTeamVN.BadmintonClub.Application.Features.Venues.Create;

public sealed record CreateVenueCommand(
    string Name,
    string? Description,
    string Address,
    decimal Latitude,
    decimal Longitude,
    TimeOnly OpenTime,
    TimeOnly CloseTime
) : ICommand<Guid>;