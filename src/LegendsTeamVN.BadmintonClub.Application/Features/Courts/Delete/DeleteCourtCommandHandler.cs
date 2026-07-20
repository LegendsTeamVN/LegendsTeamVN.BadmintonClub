using LegendsTeamVN.BadmintonClub.Domain.Entities;
using LegendsTeamVN.BadmintonClub.Domain.Repositories;
using LegendsTeamVN.Core.Application.Messaging.CQRS;
using LegendsTeamVN.Core.Utilities.Results;

namespace LegendsTeamVN.BadmintonClub.Application.Features.Courts.Delete;

public sealed class DeleteCourtCommandHandler(ICourtRepository courtRepository) : ICommandHandler<DeleteCourtCommand>
{
    public async Task<Result> Handle(DeleteCourtCommand request, CancellationToken cancellationToken)
    {
        var court = await courtRepository.FindByIdAsync(request.Id, cancellationToken);
        
        if (court is null)
        {
            return Result.Failure(Error.NotFound("Court.NotFound", "The court with the specified ID was not found."));
        }

        courtRepository.Remove(court);
        
        return Result.Success();
    }
}
