using LegendsTeamVN.Core.Application.Messaging.CQRS;

namespace LegendsTeamVN.BadmintonClub.Application.Features.Venues.Delete;

public sealed record DeleteVenueCommand(
    Guid Id
) : ICommand;