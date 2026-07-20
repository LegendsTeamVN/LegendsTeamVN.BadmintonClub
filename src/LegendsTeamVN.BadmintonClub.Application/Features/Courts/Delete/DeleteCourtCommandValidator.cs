using FluentValidation;

namespace LegendsTeamVN.BadmintonClub.Application.Features.Courts.Delete;

public sealed class DeleteCourtCommandValidator : AbstractValidator<DeleteCourtCommand>
{
    public DeleteCourtCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Court ID is required.");
    }
}
