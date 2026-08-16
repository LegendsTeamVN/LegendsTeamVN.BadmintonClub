using FluentValidation;
namespace LegendsTeamVN.BadmintonClub.Application.Features.Venues.GetById;
public sealed class GetVenueByIdQueryValidator : AbstractValidator<GetVenueByIdQuery>{
    public GetVenueByIdQueryValidator(){
        RuleFor(x=>x.Id)
            .NotEmpty().WithMessage("Venue id is required.");
    }
}