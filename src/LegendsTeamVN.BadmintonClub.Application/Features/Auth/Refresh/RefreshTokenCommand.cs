using LegendsTeamVN.Core.Application.Messaging.CQRS;
using LegendsTeamVN.Core.Identity.DTOs;

namespace LegendsTeamVN.BadmintonClub.Application.Features.Auth.Refresh;

public record RefreshTokenCommand(string AccessToken, string RefreshToken) : ICommand<AuthenticationResponse>;
