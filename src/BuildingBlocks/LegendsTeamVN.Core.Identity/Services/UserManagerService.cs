using System.Security.Claims;
using LegendsTeamVN.Core.Identity.Abstractions;
using LegendsTeamVN.Core.Identity.Entities;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LegendsTeamVN.Core.Identity.Services;

public sealed class UserManagerService(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager) : IUserManagerService
{
    public IQueryable<AppUser> GetUsersQueryable()
    {
        return userManager.Users;
    }

    public async Task<AppUser?> FindByIdAsync(Guid userId)
    {
        return await userManager.FindByIdAsync(userId.ToString());
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

        // Get permissions from user claims
        var userClaims = await userManager.GetClaimsAsync(user);
        permissions.AddRange(userClaims.Where(c => c.Type == "Permission").Select(c => c.Value));

        // Get permissions from role claims in AppRoleClaims (AspNetRoleClaims)
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

    public async Task<(bool Succeeded, IEnumerable<string> Errors)> UpdateUserAsync(Guid userId, string email, string? userName, string? phoneNumber)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());
        if (user == null) return (false, new[] { "User not found." });

        user.Email = email;
        user.UserName = userName ?? email;
        user.PhoneNumber = phoneNumber;

        var result = await userManager.UpdateAsync(user);
        return (result.Succeeded, result.Errors.Select(e => e.Description));
    }

    public async Task<(bool Succeeded, IEnumerable<string> Errors)> DeleteUserAsync(Guid userId)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());
        if (user == null) return (false, new[] { "User not found." });

        var result = await userManager.DeleteAsync(user);
        return (result.Succeeded, result.Errors.Select(e => e.Description));
    }

    public async Task<(bool Succeeded, Guid? UserId)> CheckPasswordAsync(string emailOrUserName, string password)
    {
        var user = await userManager.FindByEmailAsync(emailOrUserName) 
                ?? await userManager.FindByNameAsync(emailOrUserName);
        if (user == null)
            return (false, null);

        var success = await userManager.CheckPasswordAsync(user, password);
        return (success, success ? user.Id : null);
    }

    public async Task LockUserAsync(Guid userId, TimeSpan duration)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());
        if (user != null)
        {
            await userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow.Add(duration));
        }
    }

    public async Task UnlockUserAsync(Guid userId)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());
        if (user != null)
        {
            await userManager.SetLockoutEndDateAsync(user, null);
        }
    }

    public async Task RemoveFromRolesAsync(Guid userId, IEnumerable<string> roles)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());
        if (user != null)
        {
            await userManager.RemoveFromRolesAsync(user, roles);
        }
    }

    public async Task AddToRolesAsync(Guid userId, IEnumerable<string> roles)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());
        if (user != null)
        {
            await userManager.AddToRolesAsync(user, roles);
        }
    }

    public async Task<(bool Succeeded, IEnumerable<string> Errors)> ResetPasswordAsync(Guid userId, string newPassword)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());
        if (user == null) return (false, new[] { "User not found." });

        var removeResult = await userManager.RemovePasswordAsync(user);
        if (!removeResult.Succeeded && await userManager.HasPasswordAsync(user))
        {
            return (false, removeResult.Errors.Select(e => e.Description));
        }

        var addResult = await userManager.AddPasswordAsync(user, newPassword);
        return (addResult.Succeeded, addResult.Errors.Select(e => e.Description));
    }

    // Role Management Implementation
    public async Task<List<AppRole>> GetRolesListAsync()
    {
        return await roleManager.Roles.ToListAsync();
    }

    public async Task<AppRole?> FindRoleByIdAsync(Guid roleId)
    {
        return await roleManager.FindByIdAsync(roleId.ToString());
    }

    public async Task<(bool Succeeded, IEnumerable<string> Errors)> CreateRoleAsync(string roleName, string? description)
    {
        var role = new AppRole { Name = roleName, Description = description };
        var result = await roleManager.CreateAsync(role);
        return (result.Succeeded, result.Errors.Select(e => e.Description));
    }

    public async Task<(bool Succeeded, IEnumerable<string> Errors)> UpdateRoleAsync(Guid roleId, string roleName, string? description)
    {
        var role = await roleManager.FindByIdAsync(roleId.ToString());
        if (role == null) return (false, new[] { "Role not found." });

        role.Name = roleName;
        role.Description = description;

        var result = await roleManager.UpdateAsync(role);
        return (result.Succeeded, result.Errors.Select(e => e.Description));
    }

    public async Task<(bool Succeeded, IEnumerable<string> Errors)> DeleteRoleAsync(Guid roleId)
    {
        var role = await roleManager.FindByIdAsync(roleId.ToString());
        if (role == null) return (false, new[] { "Role not found." });

        var result = await roleManager.DeleteAsync(role);
        return (result.Succeeded, result.Errors.Select(e => e.Description));
    }

    public async Task<IList<string>> GetRolePermissionsAsync(Guid roleId)
    {
        var role = await roleManager.FindByIdAsync(roleId.ToString());
        if (role == null) return Array.Empty<string>();

        var claims = await roleManager.GetClaimsAsync(role);
        return claims.Where(c => c.Type == "Permission").Select(c => c.Value).ToList();
    }

    public async Task<bool> UpdateRolePermissionsAsync(Guid roleId, IEnumerable<string> permissions)
    {
        var role = await roleManager.FindByIdAsync(roleId.ToString());
        if (role == null) return false;

        var newPermsSet = permissions.ToHashSet();
        var existingClaims = await roleManager.GetClaimsAsync(role);

        foreach (var claim in existingClaims.Where(c => c.Type == "Permission"))
        {
            if (!newPermsSet.Contains(claim.Value))
            {
                await roleManager.RemoveClaimAsync(role, claim);
            }
        }

        var updatedClaims = (await roleManager.GetClaimsAsync(role))
            .Where(c => c.Type == "Permission")
            .Select(c => c.Value)
            .ToHashSet();

        foreach (var perm in newPermsSet)
        {
            if (!updatedClaims.Contains(perm))
            {
                await roleManager.AddClaimAsync(role, new Claim("Permission", perm));
            }
        }

        return true;
    }
}
