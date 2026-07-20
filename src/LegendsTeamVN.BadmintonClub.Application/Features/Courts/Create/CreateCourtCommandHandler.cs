using LegendsTeamVN.BadmintonClub.Domain.Entities;
using LegendsTeamVN.BadmintonClub.Domain.Repositories;
using LegendsTeamVN.Core.Application.Messaging.CQRS;
using LegendsTeamVN.Core.Utilities.Results;

namespace LegendsTeamVN.BadmintonClub.Application.Features.Courts.Create;

public sealed class CreateCourtCommandHandler(ICourtRepository courtRepository) : ICommandHandler<CreateCourtCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateCourtCommand request, CancellationToken cancellationToken)
    {
        var existingCourt = await courtRepository.FindSingleAsync(c => c.Name == request.Name, cancellationToken);
        if (existingCourt is not null)
        {
            return Result.Failure<Guid>(Error.Conflict("Court.NameExists", "A court with this name already exists."));
        }

        var court = new Court(request.Name, request.Description, request.PricePerHour);

        courtRepository.Add(court);

        return court.Id;
    }
}
