using LegendsTeamVN.Core.Application.Messaging.CQRS;
using LegendsTeamVN.Core.Identity.Abstractions;
using LegendsTeamVN.Core.Utilities.Results;

namespace LegendsTeamVN.BadmintonClub.Application.Features.Users.Me;

public sealed class MeQueryHandler(ICurrentUserService currentUserService) : IQueryHandler<MeQuery, MeResponse>
{
    public Task<Result<MeResponse>> Handle(MeQuery request, CancellationToken cancellationToken)
    {
        if (!currentUserService.IsAuthenticated)
        {
            return Task.FromResult(Result.Failure<MeResponse>(Error.Unauthorized("User.Unauthorized", "User is not authenticated.")));
        }

        var response = new MeResponse(currentUserService.UserId ?? Guid.Empty, currentUserService.Email ?? string.Empty);
        return Task.FromResult(Result.Success(response));
    }
}
