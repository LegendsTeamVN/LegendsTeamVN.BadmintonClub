using FluentValidation;
namespace LegendsTeamVN.BadmintonClub.Application.Features.Venues.GetList;
public sealed class GetVenueQueryValidator : AbstractValidator<GetVenueQuery>{
    public GetVenueQueryValidator(){
        RuleFor(x=>x.Filter.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("Page number must be at least 1.");
        RuleFor(x=>x.Filter.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("Page size must be at least 1.");
        RuleFor(x=>x.Filter.Name)
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");
        RuleFor(x=>x.Filter.Address)
            .MaximumLength(500).WithMessage("Address must not exceed 500 characters.");
        RuleFor(x=>x.Filter.OpenTime)
            .NotEmpty().WithMessage("Open time is required.");
        RuleFor(x=>x.Filter.CloseTime)
            .NotEmpty().WithMessage("Close time is required.");
        RuleFor(x=>x.Filter.Latitude)
            .Must(x=>x>=-90 && x<=90).WithMessage("Latitude must be between -90 and 90.");
        RuleFor(x=>x.Filter.Longitude)
            .Must(x=>x>=-180 && x<=180).WithMessage("Longitude must be between -180 and 180.");
    }
}