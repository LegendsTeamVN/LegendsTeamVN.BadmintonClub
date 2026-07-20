using LegendsTeamVN.Core.Application.Caching;
using LegendsTeamVN.Core.Application.Messaging.CQRS;
using LegendsTeamVN.Core.Identity.Abstractions;
using LegendsTeamVN.Core.Identity.DTOs;
using LegendsTeamVN.Core.Utilities.Results;

namespace LegendsTeamVN.BadmintonClub.Application.Features.Auth.Login;

public sealed class LoginQueryHandler(
    IUserManagerService userManagerService, 
    IJwtTokenService jwtTokenService,
    ICacheService cacheService) : IQueryHandler<LoginQuery, AuthenticationResponse>
{
    public async Task<Result<AuthenticationResponse>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var (succeeded, userId) = await userManagerService.CheckPasswordAsync(request.Email, request.Password);

        if (!succeeded || userId == null)
        {
            return Result.Failure<AuthenticationResponse>(Error.Unauthorized("User.Unauthorized", "Invalid email or password."));
        }

        var roles = await userManagerService.GetRolesAsync(userId.Value);
        var permissions = await userManagerService.GetPermissionsAsync(userId.Value);

        var accessToken = jwtTokenService.GenerateAccessToken(userId.Value, request.Email, request.Email, roles, null, permissions);
        var refreshToken = jwtTokenService.GenerateRefreshToken();

        var authResponse = new AuthenticationResponse { AccessToken = accessToken, RefreshToken = refreshToken, RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7) };
        await cacheService.SetAsync(request.Email, authResponse, TimeSpan.FromDays(7), cancellationToken);

        return authResponse;
    }
}
