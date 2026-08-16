using FluentValidation;

namespace LegendsTeamVN.BadmintonClub.Application.Features.CourtPricings.Create;

public sealed class CreateCourtPricingCommandValidator : AbstractValidator<CreateCourtPricingCommand>
{
    public CreateCourtPricingCommandValidator()
    {
        RuleFor(x => x.VenueId)
            .NotEmpty().WithMessage("VenueId is required.");

        RuleFor(x => x.EndTime)
            .GreaterThan(x => x.StartTime).WithMessage("EndTime must be after StartTime.");

        RuleFor(x => x.PricePerHour)
            .GreaterThan(0).WithMessage("PricePerHour must be greater than zero.");

        When(x => x.DayOfWeek.HasValue, () =>
        {
            RuleFor(x => x.DayOfWeek!.Value)
                .InclusiveBetween(0, 6).WithMessage("DayOfWeek must be between 0 and 6.");
        });
    }
}
