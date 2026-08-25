using LegendsTeamVN.Core.Application.Messaging.CQRS;
using LegendsTeamVN.Core.Identity.Authorization;
using LegendsTeamVN.Core.Utilities.Results;

namespace LegendsTeamVN.BadmintonClub.Application.Features.Users.GetAllPermissions;

internal sealed class GetAllPermissionsQueryHandler : IQueryHandler<GetAllPermissionsQuery, List<PermissionGroupModel>>
{
    public Task<Result<List<PermissionGroupModel>>> Handle(GetAllPermissionsQuery request, CancellationToken cancellationToken)
    {
        var groups = AppPermissions.GetAllPermissionGroups();
        return Task.FromResult(Result.Success(groups));
    }
}
