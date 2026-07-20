using LegendsTeamVN.Core.Application.Messaging.CQRS;
using LegendsTeamVN.Core.Application.Caching;
using LegendsTeamVN.Core.Utilities.Results;

namespace LegendsTeamVN.BadmintonClub.Application.Features.Auth.Logout;

public sealed class LogoutCommandHandler(ICacheService cacheService) : ICommandHandler<LogoutCommand>
{
    public async Task<Result> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        await cacheService.RemoveAsync(request.Email, cancellationToken);
        return Result.Success();
    }
}
