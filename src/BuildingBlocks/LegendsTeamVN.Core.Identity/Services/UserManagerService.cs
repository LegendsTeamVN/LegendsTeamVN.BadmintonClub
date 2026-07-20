using LegendsTeamVN.Core.Identity.Abstractions;
using LegendsTeamVN.Core.Identity.Entities;

using Microsoft.AspNetCore.Identity;

namespace LegendsTeamVN.Core.Identity.Services;

public sealed class UserManagerService(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager) : IUserManagerService
{
    public IQueryable<AppUser> GetUsersQueryable()
    {
        return userManager.Users;
    }
    public async Task<string?> GetUserEmailAsync(Guid userId)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());
        return user?.Email;
    }

    public async Task<bool> HasRoleAsync(Guid userId, string role)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());
        if (user == null) return false;
        return await userManager.IsInRoleAsync(user, role);
    }

    public async Task<IList<string>> GetRolesAsync(Guid userId)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());
        if (user == null) return Array.Empty<string>();
        return await userManager.GetRolesAsync(user);
    }

    public async Task<IList<string>> GetPermissionsAsync(Guid userId)
    {
        var permissions = new List<string>();
        var user = await userManager.FindByIdAsync(userId.ToString());
        if (user == null) return permissions;

        // Get claims from user
        var userClaims = await userManager.GetClaimsAsync(user);
        permissions.AddRange(userClaims.Where(c => c.Type == "Permission").Select(c => c.Value));

        // Get claims from roles
        var roles = await userManager.GetRolesAsync(user);
        foreach (var roleName in roles)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            if (role != null)
            {
                var roleClaims = await roleManager.GetClaimsAsync(role);
                permissions.AddRange(roleClaims.Where(c => c.Type == "Permission").Select(c => c.Value));
            }
        }

        return permissions.Distinct().ToList();
    }

    public async Task<(bool Succeeded, Guid? UserId, IEnumerable<string> Errors)> CreateUserAsync(string email, string password)
    {
        var user = new AppUser { UserName = email, Email = email };
        var result = await userManager.CreateAsync(user, password);
        
        return (result.Succeeded, user.Id, result.Errors.Select(e => e.Description));
    }

    public async Task<(bool Succeeded, Guid? UserId)> CheckPasswordAsync(string email, string password)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user == null)
            return (false, null);

        var success = await userManager.CheckPasswordAsync(user, password);
        return (success, success ? user.Id : null);
    }
}
