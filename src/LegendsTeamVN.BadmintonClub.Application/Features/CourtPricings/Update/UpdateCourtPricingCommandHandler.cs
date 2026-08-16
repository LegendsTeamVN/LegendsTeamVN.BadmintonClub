using LegendsTeamVN.BadmintonClub.Domain.Repositories;
using LegendsTeamVN.Core.Application.Messaging.CQRS;
using LegendsTeamVN.Core.Utilities.Results;

namespace LegendsTeamVN.BadmintonClub.Application.Features.CourtPricings.Update;

public sealed class UpdateCourtPricingCommandHandler(
    ICourtPricingRepository courtPricingRepository
) : ICommandHandler<UpdateCourtPricingCommand>
{
    public async Task<Result> Handle(UpdateCourtPricingCommand request, CancellationToken cancellationToken)
    {
        var pricing = await courtPricingRepository.FindByIdAsync(request.Id, cancellationToken);

        if (pricing is null)
        {
            return Result.Failure(Error.NotFound("CourtPricing.NotFound", "The court pricing rule with the specified ID was not found."));
        }

        pricing.UpdatePricing(
            request.StartTime,
            request.EndTime,
            request.PricePerHour,
            request.DayOfWeek,
            request.IsPeakHour
        );

        courtPricingRepository.Update(pricing);

        return Result.Success();
    }
}
