using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

using LegendsTeamVN.Core.Application.Messaging.CQRS;
using LegendsTeamVN.Core.Identity.Abstractions;
using LegendsTeamVN.Core.Utilities.Results;
using LegendsTeamVN.Core.Application.Caching;
using LegendsTeamVN.Core.Identity.DTOs;

namespace LegendsTeamVN.BadmintonClub.Application.Features.Auth.Refresh;

public sealed class RefreshTokenCommandHandler(
    IUserManagerService userManagerService,
    IJwtTokenService jwtTokenService,
    ICacheService cacheService) : ICommandHandler<RefreshTokenCommand, AuthenticationResponse>
{
    public async Task<Result<AuthenticationResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        ClaimsPrincipal principal;
        try
        {
            principal = jwtTokenService.GetPrincipalFromExpiredToken(request.AccessToken);
        }
        catch
        {
            return Result.Failure<AuthenticationResponse>(Error.Unauthorized("Token.Invalid", "Invalid access token."));
        }

        var email = principal.FindFirst(JwtRegisteredClaimNames.Email)?.Value ?? principal.FindFirst(ClaimTypes.Email)?.Value;
        if (string.IsNullOrEmpty(email))
        {
            return Result.Failure<AuthenticationResponse>(Error.Unauthorized("Token.Invalid", "Token does not contain an email claim."));
        }

        var userIdString = principal.FindFirst(JwtRegisteredClaimNames.Sub)?.Value ?? principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!Guid.TryParse(userIdString, out var userId))
        {
            return Result.Failure<AuthenticationResponse>(Error.Unauthorized("Token.Invalid", "Token does not contain a valid user id."));
        }

        var userName = principal.FindFirst(JwtRegisteredClaimNames.Name)?.Value ?? principal.FindFirst(ClaimTypes.Name)?.Value ?? email;
        var cachedSession = await cacheService.GetAsync<AuthenticationResponse>(email, cancellationToken);
        if (cachedSession is null)
        {
            return Result.Failure<AuthenticationResponse>(Error.Unauthorized("Session.Expired", "Refresh token has expired or session does not exist."));
        }

        if (cachedSession.RefreshToken != request.RefreshToken)
        {
            return Result.Failure<AuthenticationResponse>(Error.Unauthorized("Token.Invalid", "Invalid refresh token."));
        }

        if (cachedSession.RefreshTokenExpiryTime < DateTime.UtcNow)
        {
            return Result.Failure<AuthenticationResponse>(Error.Unauthorized("Token.Expired", "Refresh token has expired."));
        }

        var roles = await userManagerService.GetRolesAsync(userId);
        var permissions = await userManagerService.GetPermissionsAsync(userId);

        var newAccessToken = jwtTokenService.GenerateAccessToken(userId, email, userName, roles, null, permissions);
        var newRefreshToken = jwtTokenService.GenerateRefreshToken();

        var authResponse = new AuthenticationResponse 
        { 
            AccessToken = newAccessToken, 
            RefreshToken = newRefreshToken, 
            RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7) 
        };

        await cacheService.SetAsync(email, authResponse, TimeSpan.FromDays(7), cancellationToken);

        return authResponse;
    }
}
