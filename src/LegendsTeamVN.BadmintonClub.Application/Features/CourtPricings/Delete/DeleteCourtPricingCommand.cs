using LegendsTeamVN.Core.Application.Messaging.CQRS;

namespace LegendsTeamVN.BadmintonClub.Application.Features.CourtPricings.Delete;

public sealed record DeleteCourtPricingCommand(Guid Id) : ICommand;
