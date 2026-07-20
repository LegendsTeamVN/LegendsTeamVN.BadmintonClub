using System.Security.Claims;

namespace LegendsTeamVN.Core.Identity.Abstractions;

public interface IJwtTokenService
{
    string GenerateAccessToken(Guid userId, string email, string userName, IList<string> roles, Guid? tenantId = null, IList<string>? permissions = null);
    string GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}
