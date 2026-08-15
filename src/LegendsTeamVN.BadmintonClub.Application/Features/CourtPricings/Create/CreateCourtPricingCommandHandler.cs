using LegendsTeamVN.BadmintonClub.Domain.Entities;
using LegendsTeamVN.BadmintonClub.Domain.Repositories;
using LegendsTeamVN.Core.Application.Messaging.CQRS;
using LegendsTeamVN.Core.Utilities.Results;

namespace LegendsTeamVN.BadmintonClub.Application.Features.CourtPricings.Create;

public sealed class CreateCourtPricingCommandHandler(
    ICourtPricingRepository courtPricingRepository
) : ICommandHandler<CreateCourtPricingCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateCourtPricingCommand request, CancellationToken cancellationToken)
    {
        var pricing = new CourtPricing(
            request.VenueId,
            request.StartTime,
            request.EndTime,
            request.PricePerHour,
            request.DayOfWeek,
            request.IsPeakHour
        );

        courtPricingRepository.Add(pricing);

        return pricing.Id;
    }
}
