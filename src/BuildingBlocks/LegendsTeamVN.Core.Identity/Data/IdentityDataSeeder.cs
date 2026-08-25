using System.Security.Claims;
using LegendsTeamVN.Core.Application.Data;
using LegendsTeamVN.Core.Identity.Authorization;
using LegendsTeamVN.Core.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace LegendsTeamVN.Core.Identity.Data;

internal sealed class IdentityDataSeeder(
    UserManager<AppUser> userManager,
    RoleManager<AppRole> roleManager) : IDataSeeder
{
    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        var roles = new[] { "Admin", "Manager", "User" };
        foreach (var roleName in roles)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                role = new AppRole { Name = roleName, Description = $"{roleName} role" };
                await roleManager.CreateAsync(role);
            }

            if (roleName == "Admin")
            {
                var adminPermissionsSet = AppPermissions
                    .GetAllPermissionNames(AppPermissions.GetAllPermissionGroups())
                    .ToHashSet();

                var existingRoleClaims = await roleManager.GetClaimsAsync(role);
                
                // 1. Remove obsolete claims from Admin role in AppRoleClaims table (e.g. System.Administrator)
                foreach (var claim in existingRoleClaims.Where(c => c.Type == "Permission"))
                {
                    if (!adminPermissionsSet.Contains(claim.Value))
                    {
                        await roleManager.RemoveClaimAsync(role, claim);
                    }
                }

                var updatedRoleClaims = (await roleManager.GetClaimsAsync(role))
                    .Where(c => c.Type == "Permission")
                    .Select(c => c.Value)
                    .ToHashSet();

                // 2. Add all current permissions into AppRoleClaims table for Admin role
                foreach (var perm in adminPermissionsSet)
                {
                    if (!updatedRoleClaims.Contains(perm))
                    {
                        await roleManager.AddClaimAsync(role, new Claim("Permission", perm));
                    }
                }
            }
            else if (roleName == "User")
            {
                var existingRoleClaims = await roleManager.GetClaimsAsync(role);
                if (!existingRoleClaims.Any(c => c.Type == "Permission" && c.Value == "Courts.Read"))
                {
                    await roleManager.AddClaimAsync(role, new Claim("Permission", "Courts.Read"));
                }
            }
        }

        var defaultAdmins = new (string UserName, string Email, string Password)[]
        {
            ("admin", "admin@admin.com", "admin"),
            ("admin@l7ungdz.id.vn", "admin@l7ungdz.id.vn", "admin")
        };

        foreach (var adminInfo in defaultAdmins)
        {
            var existingUser = await userManager.FindByEmailAsync(adminInfo.Email) 
                            ?? await userManager.FindByNameAsync(adminInfo.UserName);

            if (existingUser == null)
            {
                var adminUser = new AppUser
                {
                    UserName = adminInfo.UserName,
                    Email = adminInfo.Email,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, adminInfo.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
            else
            {
                var userRoles = await userManager.GetRolesAsync(existingUser);
                if (!userRoles.Contains("Admin"))
                {
                    await userManager.AddToRoleAsync(existingUser, "Admin");
                }

                // Reset password to 'admin'
                var removeResult = await userManager.RemovePasswordAsync(existingUser);
                if (removeResult.Succeeded || !await userManager.HasPasswordAsync(existingUser))
                {
                    await userManager.AddPasswordAsync(existingUser, adminInfo.Password);
                }

                // Purge direct user claims so permissions come dynamically from AppRoleClaims
                var userClaims = await userManager.GetClaimsAsync(existingUser);
                foreach (var claim in userClaims.Where(c => c.Type == "Permission"))
                {
                    await userManager.RemoveClaimAsync(existingUser, claim);
                }
            }
        }
    }
}
