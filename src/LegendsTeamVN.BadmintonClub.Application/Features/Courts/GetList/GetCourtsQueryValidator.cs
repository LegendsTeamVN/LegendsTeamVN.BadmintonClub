using FluentValidation;

namespace LegendsTeamVN.BadmintonClub.Application.Features.Courts.GetList;

public sealed class GetCourtsQueryValidator : AbstractValidator<GetCourtsQuery>
{
    public GetCourtsQueryValidator()
    {
        RuleFor(x => x.Filter.MinPrice)
            .GreaterThanOrEqualTo(0).When(x => x.Filter.MinPrice.HasValue)
            .WithMessage("MinPrice must be greater than or equal to 0.");

        RuleFor(x => x.Filter.MaxPrice)
            .GreaterThanOrEqualTo(0).When(x => x.Filter.MaxPrice.HasValue)
            .WithMessage("MaxPrice must be greater than or equal to 0.");

        RuleFor(x => x.Filter.MaxPrice)
            .GreaterThanOrEqualTo(x => x.Filter.MinPrice)
            .When(x => x.Filter.MinPrice.HasValue && x.Filter.MaxPrice.HasValue)
            .WithMessage("MaxPrice must be greater than or equal to MinPrice.");
    }
}
