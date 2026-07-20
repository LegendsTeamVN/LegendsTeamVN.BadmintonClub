using Microsoft.AspNetCore.Builder;

namespace LegendsTeamVN.Core.Identity.Authorization;

public static class PermissionAuthorizationExtensions
{
    public static TBuilder RequirePermission<TBuilder>(this TBuilder builder, string permission) 
        where TBuilder : IEndpointConventionBuilder
    {
        return builder.RequireAuthorization(policy => policy.RequireClaim("permissions", permission));
    }
}
