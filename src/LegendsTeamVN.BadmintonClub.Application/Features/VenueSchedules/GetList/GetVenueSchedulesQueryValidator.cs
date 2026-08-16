using FluentValidation;

namespace LegendsTeamVN.BadmintonClub.Application.Features.VenueSchedules.GetList;

public sealed class GetVenueSchedulesQueryValidator : AbstractValidator<GetVenueSchedulesQuery>
{
    public GetVenueSchedulesQueryValidator()
    {
        RuleFor(x => x.Filter.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber must be greater than or equal to 1.");

        RuleFor(x => x.Filter.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize must be greater than or equal to 1.");

        When(x => x.Filter.DayOfWeek.HasValue, () =>
        {
            RuleFor(x => x.Filter.DayOfWeek!.Value)
                .InclusiveBetween(0, 6).WithMessage("DayOfWeek must be between 0 and 6.");
        });
    }
}
