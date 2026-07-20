using LegendsTeamVN.BadmintonClub.Domain.Repositories;
using LegendsTeamVN.Core.Application.Messaging.CQRS;
using LegendsTeamVN.Core.Utilities.Results;

namespace LegendsTeamVN.BadmintonClub.Application.Features.Courts.Update;

public sealed class UpdateCourtCommandHandler(ICourtRepository courtRepository) : ICommandHandler<UpdateCourtCommand>
{
    public async Task<Result> Handle(UpdateCourtCommand request, CancellationToken cancellationToken)
    {
        var court = await courtRepository.FindByIdAsync(request.Id, cancellationToken);
        
        if (court is null)
        {
            return Result.Failure(Error.NotFound("Court.NotFound", "The court with the specified ID was not found."));
        }

        court.UpdateDetails(request.Name, request.Description, request.PricePerHour);
        court.SetAvailability(request.IsAvailable);

        courtRepository.Update(court);
        
        return Result.Success();
    }
}
