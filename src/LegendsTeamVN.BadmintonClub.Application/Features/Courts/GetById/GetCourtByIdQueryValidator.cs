using FluentValidation;

namespace LegendsTeamVN.BadmintonClub.Application.Features.Courts.GetById;

public sealed class GetCourtByIdQueryValidator : AbstractValidator<GetCourtByIdQuery>
{
    public GetCourtByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Court ID is required.");
    }
}
