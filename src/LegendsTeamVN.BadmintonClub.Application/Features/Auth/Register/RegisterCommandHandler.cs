using LegendsTeamVN.Core.Application.Messaging.CQRS;
using LegendsTeamVN.Core.Identity.Abstractions;
using LegendsTeamVN.Core.Utilities.Results;

namespace LegendsTeamVN.BadmintonClub.Application.Features.Auth.Register;

public sealed class RegisterCommandHandler(IUserManagerService userManagerService) : ICommandHandler<RegisterCommand, Guid>
{
    public async Task<Result<Guid>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var (succeeded, userId, errors) = await userManagerService.CreateUserAsync(request.Email, request.Password);

        if (!succeeded || userId == null)
        {
            return Result.Failure<Guid>(Error.Validation("User.RegisterFailed", string.Join(", ", errors)));
        }

        return userId.Value;
    }
}
