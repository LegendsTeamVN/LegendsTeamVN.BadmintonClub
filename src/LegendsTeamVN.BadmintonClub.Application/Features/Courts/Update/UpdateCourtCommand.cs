using LegendsTeamVN.Core.Application.Messaging.CQRS;

namespace LegendsTeamVN.BadmintonClub.Application.Features.Courts.Update;

public sealed record UpdateCourtCommand(
    Guid Id,
    string Name,
    string? Description,
    decimal PricePerHour,
    bool IsAvailable
) : ICommand;
