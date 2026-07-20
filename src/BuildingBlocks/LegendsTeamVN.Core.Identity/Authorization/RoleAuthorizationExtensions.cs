using Microsoft.AspNetCore.Builder;

namespace LegendsTeamVN.Core.Identity.Authorization;

public static class RoleAuthorizationExtensions
{
    public static TBuilder RequireRole<TBuilder>(this TBuilder builder, string role)
        where TBuilder : IEndpointConventionBuilder
    {
        return builder.RequireAuthorization(policy => policy.RequireRole(role));
    }
}
