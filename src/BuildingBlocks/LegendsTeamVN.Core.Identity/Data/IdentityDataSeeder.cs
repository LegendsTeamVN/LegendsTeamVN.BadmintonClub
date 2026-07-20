using System.Security.Claims;
using LegendsTeamVN.Core.Application.Data;
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
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                var role = new AppRole { Name = roleName, Description = $"{roleName} role" };
                await roleManager.CreateAsync(role);

                if (roleName == "Admin")
                {
                    await roleManager.AddClaimAsync(role, new Claim("Permission", "Courts.Create"));
                    await roleManager.AddClaimAsync(role, new Claim("Permission", "Courts.Update"));
                    await roleManager.AddClaimAsync(role, new Claim("Permission", "Courts.Delete"));
                    await roleManager.AddClaimAsync(role, new Claim("Permission", "Courts.Read"));
                }
                else if (roleName == "User")
                {
                    await roleManager.AddClaimAsync(role, new Claim("Permission", "Courts.Read"));
                }
            }
        }

        var adminEmail = "admin@l7ungdz.id.vn";
        if (await userManager.FindByEmailAsync(adminEmail) == null)
        {
            var adminUser = new AppUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(adminUser, "Admin@123");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");

                await userManager.AddClaimAsync(adminUser, new Claim("Permission", "System.Administrator"));
            }
        }
    }
}
