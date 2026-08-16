using FluentValidation;

namespace LegendsTeamVN.BadmintonClub.Application.Features.VenueSchedules.Update;

public sealed class UpdateVenueScheduleCommandValidator : AbstractValidator<UpdateVenueScheduleCommand>
{
    public UpdateVenueScheduleCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");

        RuleFor(x => x)
            .Must(x => x.IsClosed || x.CloseTime > x.OpenTime)
            .WithMessage("CloseTime must be after OpenTime when venue is not closed.");
    }
}
