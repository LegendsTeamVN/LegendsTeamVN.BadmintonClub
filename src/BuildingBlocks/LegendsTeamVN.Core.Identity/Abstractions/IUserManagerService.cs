using LegendsTeamVN.Core.Identity.Entities;

namespace LegendsTeamVN.Core.Identity.Abstractions;

public interface IUserManagerService
{
    IQueryable<AppUser> GetUsersQueryable();
    Task<string?> GetUserEmailAsync(Guid userId);
    Task<bool> HasRoleAsync(Guid userId, string role);
    Task<IList<string>> GetRolesAsync(Guid userId);
    Task<IList<string>> GetPermissionsAsync(Guid userId);
    Task<(bool Succeeded, Guid? UserId, IEnumerable<string> Errors)> CreateUserAsync(string email, string password);
    Task<(bool Succeeded, Guid? UserId)> CheckPasswordAsync(string email, string password);
}
