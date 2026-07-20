using LegendsTeamVN.Core.Application.Messaging.CQRS;
using LegendsTeamVN.Core.Identity.DTOs;

namespace LegendsTeamVN.BadmintonClub.Application.Features.Auth.Login;

public sealed record LoginQuery(
    string Email,
    string Password
) : IQuery<AuthenticationResponse>;
