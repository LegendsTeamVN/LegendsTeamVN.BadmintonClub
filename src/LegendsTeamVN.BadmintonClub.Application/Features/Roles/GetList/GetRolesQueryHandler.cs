using LegendsTeamVN.BadmintonClub.Application.DTOs.Roles;
using LegendsTeamVN.Core.Application.Messaging.CQRS;
using LegendsTeamVN.Core.Identity.Abstractions;
using LegendsTeamVN.Core.Identity.Authorization;
using LegendsTeamVN.Core.Utilities.Results;

namespace LegendsTeamVN.BadmintonClub.Application.Features.Roles.GetList;

internal sealed class GetRolesQueryHandler(IUserManagerService userManagerService)
    : IQueryHandler<GetRolesQuery, List<RoleResponse>>
{
    public async Task<Result<List<RoleResponse>>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = await userManagerService.GetRolesListAsync();
        var result = new List<RoleResponse>();

        foreach (var role in roles)
        {
            var permNames = await userManagerService.GetRolePermissionsAsync(role.Id);
            var groupedPermissions = AppPermissions.GetGroupedPermissions(permNames);

            result.Add(new RoleResponse(role.Id, role.Name ?? string.Empty, role.Description, groupedPermissions));
        }

        return Result.Success(result);
    }
}
