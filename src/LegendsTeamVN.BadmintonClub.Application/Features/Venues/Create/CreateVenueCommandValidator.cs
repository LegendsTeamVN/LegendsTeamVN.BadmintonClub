using FluentValidation;
namespace LegendsTeamVN.BadmintonClub.Application.Features.Venues.Create;
public sealed class CreateVenueCommandValidator : AbstractValidator<CreateVenueCommand>{
    public CreateVenueCommandValidator(){
        RuleFor(x=>x.Name)
            .NotEmpty().WithMessage("Venue name is required.")
            .MaximumLength(100).WithMessage("Venue name must not exceed 100 characters.");
        RuleFor(x=>x.Description)
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");
        RuleFor(x=>x.Address)
            .NotEmpty().WithMessage("Address is required.")
            .MaximumLength(500).WithMessage("Address must not exceed 500 characters.");
        RuleFor(x=>x.Latitude)
            .NotEmpty().WithMessage("Latitude is required.")
            .Must(x=>x>=-90 && x<=90).WithMessage("Latitude must be between -90 and 90.");
        RuleFor(x=>x.Longitude)
            .NotEmpty().WithMessage("Longitude is required.")
            .Must(x=>x>=-180 && x<=180).WithMessage("Longitude must be between -180 and 180.");
        RuleFor(x=>x.OpenTime)
            .NotEmpty().WithMessage("Open time is required.");
        RuleFor(x=>x.CloseTime)
            .NotEmpty().WithMessage("Close time is required.")
            .Must((command,closeTime)=>closeTime>command.OpenTime).WithMessage("Close time must be after open time.");
    }
}