using FluentValidation;

namespace LegendsTeamVN.BadmintonClub.Application.Features.Courts.Update;

public sealed class UpdateCourtCommandValidator : AbstractValidator<UpdateCourtCommand>
{
    public UpdateCourtCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Court ID is required.");
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100).WithMessage("Name is required and must not exceed 100 characters.");
        RuleFor(x => x.PricePerHour).GreaterThan(0).WithMessage("Price per hour must be greater than zero.");
    }
}
