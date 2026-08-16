using FluentValidation;

namespace LegendsTeamVN.BadmintonClub.Application.Features.VenueSchedules.Create;

public sealed class CreateVenueScheduleCommandValidator : AbstractValidator<CreateVenueScheduleCommand>
{
    public CreateVenueScheduleCommandValidator()
    {
        RuleFor(x => x.VenueId)
            .NotEmpty().WithMessage("VenueId is required.");

        RuleFor(x => x.DayOfWeek)
            .InclusiveBetween(0, 6).WithMessage("DayOfWeek must be between 0 and 6.");

        RuleFor(x => x)
            .Must(x => x.IsClosed || x.CloseTime > x.OpenTime)
            .WithMessage("CloseTime must be after OpenTime when venue is not closed.");
    }
}
