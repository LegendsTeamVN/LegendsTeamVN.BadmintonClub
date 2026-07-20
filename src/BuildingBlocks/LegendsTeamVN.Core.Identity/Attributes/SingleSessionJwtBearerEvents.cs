using System.Security.Claims;
using LegendsTeamVN.Core.Identity.DTOs;
using LegendsTeamVN.Core.Application.Caching;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace LegendsTeamVN.Core.Identity.Attributes;

public class SingleSessionJwtBearerEvents(ICacheService cacheService) : JwtBearerEvents
{
    private readonly ICacheService _cacheService = cacheService;

    public override Task AuthenticationFailed(AuthenticationFailedContext context)
    {
        if (context.Exception is SecurityTokenExpiredException)
        {
            context.Response.Headers["IS-TOKEN-EXPIRED"] = "true";
        }

        return base.AuthenticationFailed(context);
    }

    public override async Task TokenValidated(TokenValidatedContext context)
    {
        if (context.SecurityToken is not JsonWebToken jsonWebToken)
        {
            context.Fail("Authentication failed: invalid token format.");
            return;
        }

        var requestToken = jsonWebToken.EncodedToken;

        var emailKey = context.Principal?.FindFirst(ClaimTypes.Email)?.Value;
        if (string.IsNullOrEmpty(emailKey))
        {
            context.Fail("Authentication failed: email claim is missing from token.");
            return;
        }

        var authenticated = await _cacheService.GetAsync<AuthenticationResponse>(emailKey);

        if (authenticated is null || authenticated.AccessToken != requestToken)
        {
            context.Response.Headers["IS-TOKEN-REVOKED"] = "true";
            context.Fail("Authentication failed: token has been revoked or session expired.");
            return;
        }

        await base.TokenValidated(context);
    }
}
