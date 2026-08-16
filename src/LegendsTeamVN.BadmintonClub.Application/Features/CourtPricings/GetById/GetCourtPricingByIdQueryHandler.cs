using LegendsTeamVN.BadmintonClub.Application.DTOs.CourtPricings.Responses;
using LegendsTeamVN.BadmintonClub.Domain.Repositories;
using LegendsTeamVN.Core.Application.Messaging.CQRS;
using LegendsTeamVN.Core.Utilities.Results;

namespace LegendsTeamVN.BadmintonClub.Application.Features.CourtPricings.GetById;

public sealed class GetCourtPricingByIdQueryHandler(
    ICourtPricingRepository courtPricingRepository
) : IQueryHandler<GetCourtPricingByIdQuery, CourtPricingResponse>
{
    public async Task<Result<CourtPricingResponse>> Handle(GetCourtPricingByIdQuery request, CancellationToken cancellationToken)
    {
        var pricing = await courtPricingRepository.FindByIdAsync(request.Id, cancellationToken);

        if (pricing is null)
        {
            return Result.Failure<CourtPricingResponse>(Error.NotFound("CourtPricing.NotFound", "The court pricing rule with the specified ID was not found."));
        }

        var response = new CourtPricingResponse(
            pricing.Id,
            pricing.VenueId,
            pricing.StartTime,
            pricing.EndTime,
            pricing.PricePerHour,
            pricing.DayOfWeek,
            pricing.IsPeakHour,
            pricing.CreatedOnUtc,
            pricing.ModifiedOnUtc
        );

        return Result.Success(response);
    }
}
