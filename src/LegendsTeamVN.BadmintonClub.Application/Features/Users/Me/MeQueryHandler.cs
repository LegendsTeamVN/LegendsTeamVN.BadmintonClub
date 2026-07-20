using LegendsTeamVN.BadmintonClub.Application.DTOs.Users.Responses;
using LegendsTeamVN.Core.Application.Messaging.CQRS;
using LegendsTeamVN.Core.Identity.Abstractions;
using LegendsTeamVN.Core.Utilities.Results;

namespace LegendsTeamVN.BadmintonClub.Application.Features.Users.Me;

public sealed class MeQueryHandler(ICurrentUserService currentUserService) : IQueryHandler<MeQuery, UserResponse>
{
    public Task<Result<UserResponse>> Handle(MeQuery request, CancellationToken cancellationToken)
    {
        if (!currentUserService.IsAuthenticated)
        {
            return Task.FromResult(Result.Failure<UserResponse>(Error.Unauthorized("User.Unauthorized", "User is not authenticated.")));
        }

        var response = new UserResponse(currentUserService.UserId ?? Guid.Empty, currentUserService.Email ?? string.Empty, currentUserService.UserName, currentUserService.Roles, currentUserService.Permissions);
        return Task.FromResult(Result.Success(response));
    }
}
