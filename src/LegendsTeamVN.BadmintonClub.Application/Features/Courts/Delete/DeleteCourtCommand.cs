using LegendsTeamVN.Core.Application.Messaging.CQRS;

namespace LegendsTeamVN.BadmintonClub.Application.Features.Courts.Delete;

public sealed record DeleteCourtCommand(Guid Id) : ICommand;
