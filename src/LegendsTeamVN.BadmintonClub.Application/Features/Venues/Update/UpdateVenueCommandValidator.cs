using FluentValidation;
namespace LegendsTeamVN.BadmintonClub.Application.Features.Venues.Update;
public sealed class UpdateVenueCommandValidator : AbstractValidator<UpdateVenueCommand>{
    public UpdateVenueCommandValidator(){
        RuleFor(x=>x.Id)
            .NotEmpty().WithMessage("Venue id is required.");
        RuleFor(x=>x.Name)
            .NotEmpty().WithMessage("Venue name is required.");
        RuleFor(x=>x.Address)
            .NotEmpty().WithMessage("Venue address is required.");
        RuleFor(x=>x.OpenTime)
            .Must(x => x != TimeOnly.MinValue).WithMessage("Venue open time is required.");
        RuleFor(x=>x.CloseTime)
            .Must(x => x != TimeOnly.MinValue).WithMessage("Venue close time is required.")
            .GreaterThan(x=>x.OpenTime).WithMessage("Close time must be after open time.");
        RuleFor(x=>x.Latitude)
            .Must(x=>x>=-90 && x<=90).WithMessage("Latitude must be between -90 and 90.");
        RuleFor(x=>x.Longitude)
            .Must(x=>x>=-180 && x<=180).WithMessage("Longitude must be between -180 and 180.");
    }
}