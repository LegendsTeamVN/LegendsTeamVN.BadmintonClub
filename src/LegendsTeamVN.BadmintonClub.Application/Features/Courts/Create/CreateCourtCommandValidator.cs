using FluentValidation;

namespace LegendsTeamVN.BadmintonClub.Application.Features.Courts.Create;

public sealed class CreateCourtCommandValidator : AbstractValidator<CreateCourtCommand>
{
    public CreateCourtCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Court name is required.")
            .MaximumLength(100).WithMessage("Court name must not exceed 100 characters.");
        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");
        RuleFor(x => x.PricePerHour)
            .GreaterThan(0).WithMessage("Price per hour must be greater than zero.");
    }
}
