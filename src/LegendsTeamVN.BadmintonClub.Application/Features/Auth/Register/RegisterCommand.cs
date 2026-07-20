using LegendsTeamVN.Core.Application.Messaging.CQRS;

namespace LegendsTeamVN.BadmintonClub.Application.Features.Auth.Register;

public sealed record RegisterCommand(
    string Email,
    string Password
) : ICommand<Guid>;
