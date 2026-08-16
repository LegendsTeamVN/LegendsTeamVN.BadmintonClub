using FluentValidation;
namespace LegendsTeamVN.BadmintonClub.Application.Features.Venues.Delete;
public sealed class DeleteVenueCommandValidator : AbstractValidator<DeleteVenueCommand>{
    public DeleteVenueCommandValidator(){
        RuleFor(x=>x.Id)
            .NotEmpty().WithMessage("Venue id is required.");
    }
}