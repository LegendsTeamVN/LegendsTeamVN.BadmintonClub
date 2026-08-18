using FluentValidation;
namespace LegendsTeamVN.BadmintonClub.Application.Features.Venues.GetList;
public sealed class GetVenueQueryValidator : AbstractValidator<GetVenueQuery>{
    public GetVenueQueryValidator(){
        RuleFor(x=>x.Filter.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("Page number must be at least 1.");
        RuleFor(x=>x.Filter.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("Page size must be at least 1.");
        RuleFor(x=>x.Filter.Name)
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.")
            .When(x => !string.IsNullOrEmpty(x.Filter.Name));
        RuleFor(x=>x.Filter.Address)
            .MaximumLength(500).WithMessage("Address must not exceed 500 characters.")
            .When(x => !string.IsNullOrEmpty(x.Filter.Address));
        RuleFor(x=>x.Filter.Latitude)
            .Must(x=>x>=-90 && x<=90).WithMessage("Latitude must be between -90 and 90.")
            .When(x => x.Filter.Latitude.HasValue);
        RuleFor(x=>x.Filter.Longitude)
            .Must(x=>x>=-180 && x<=180).WithMessage("Longitude must be between -180 and 180.")
            .When(x => x.Filter.Longitude.HasValue);
    }
}