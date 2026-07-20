using FluentValidation;

namespace LegendsTeamVN.BadmintonClub.Application.Features.Users.GetList;

public sealed class GetUsersQueryValidator : AbstractValidator<GetUsersQuery>
{
    public GetUsersQueryValidator()
    {
        RuleFor(x => x.Filter.Email).EmailAddress().When(x => !string.IsNullOrEmpty(x.Filter.Email)).WithMessage("Invalid email format.");
        RuleFor(x => x.Filter.UserName).MaximumLength(100).When(x => !string.IsNullOrEmpty(x.Filter.UserName)).WithMessage("Username is too long.");
    }
}
