using LegendsTeamVN.BadmintonClub.Application.DTOs.Users.Responses;
using LegendsTeamVN.Core.Application.Messaging.CQRS;
using LegendsTeamVN.Core.Identity.Abstractions;
using LegendsTeamVN.Core.Utilities.Pagination;
using LegendsTeamVN.Core.Utilities.Results;

namespace LegendsTeamVN.BadmintonClub.Application.Features.Users.GetList;

public sealed class GetUsersQueryHandler(IUserManagerService userManagerService) : IQueryHandler<GetUsersQuery, PagedResult<UserResponse>>
{
    public async Task<Result<PagedResult<UserResponse>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var filter = request.Filter;
        var query = userManagerService.GetUsersQueryable();

        query = query.ApplyBaseFilter(filter, "Email", "UserName");

        var pagedResult = await query
            .Select(user => new UserResponse(user.Id, user.Email!, user.UserName))
            .ToPagedResultAsync(filter.PageNumber, filter.PageSize, cancellationToken);

        return Result.Success(pagedResult);
    }
}
