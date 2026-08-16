using LegendsTeamVN.BadmintonClub.Domain.Repositories;
using LegendsTeamVN.Core.Application.Messaging.CQRS;
using LegendsTeamVN.Core.Utilities.Results;

namespace LegendsTeamVN.BadmintonClub.Application.Features.CourtPricings.Delete;

public sealed class DeleteCourtPricingCommandHandler(
    ICourtPricingRepository courtPricingRepository
) : ICommandHandler<DeleteCourtPricingCommand>
{
    public async Task<Result> Handle(DeleteCourtPricingCommand request, CancellationToken cancellationToken)
    {
        var pricing = await courtPricingRepository.FindByIdAsync(request.Id, cancellationToken);

        if (pricing is null)
        {
            return Result.Failure(Error.NotFound("CourtPricing.NotFound", "The court pricing rule with the specified ID was not found."));
        }

        courtPricingRepository.Remove(pricing);

        return Result.Success();
    }
}
