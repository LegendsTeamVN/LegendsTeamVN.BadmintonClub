using LegendsTeamVN.BadmintonClub.Domain.Entities;
using LegendsTeamVN.BadmintonClub.Domain.Repositories;
using LegendsTeamVN.Core.Application.Messaging.CQRS;
using LegendsTeamVN.Core.Utilities.Results;

namespace LegendsTeamVN.BadmintonClub.Application.Features.Venues.Delete;

public sealed class DeleteVenueCommandHandler(IVenueRepository venueRepository): ICommandHandler<DeleteVenueCommand>{
    public async Task<Result> Handle(DeleteVenueCommand request , CancellationToken cancellationToken ){
        var venue = await venueRepository.FindByIdAsync(request.Id,cancellationToken);
        if(venue is null){
            return Result.Failure(Error.NotFound("Venue.NotFound","A venue with this id does not exist."));
        }
        venueRepository.Remove(venue);
        return Result.Success();
    }
}