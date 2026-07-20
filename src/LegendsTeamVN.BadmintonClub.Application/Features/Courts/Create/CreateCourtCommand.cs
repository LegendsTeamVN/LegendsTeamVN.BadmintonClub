using LegendsTeamVN.Core.Application.Messaging.CQRS;

namespace LegendsTeamVN.BadmintonClub.Application.Features.Courts.Create;

public sealed record CreateCourtCommand(
    string Name,
    string? Description,
    decimal PricePerHour
) : ICommand<Guid>;
