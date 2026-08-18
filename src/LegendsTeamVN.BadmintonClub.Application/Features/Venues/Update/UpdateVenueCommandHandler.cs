using LegendsTeamVN.BadmintonClub.Domain.Entities;
using LegendsTeamVN.BadmintonClub.Domain.Repositories;
using LegendsTeamVN.Core.Application.Messaging.CQRS;
using LegendsTeamVN.Core.Utilities.Results;

namespace LegendsTeamVN.BadmintonClub.Application.Features.Venues.Update;

public sealed class UpdateVenueCommandHandler(IVenueRepository venueRepository): ICommandHandler<UpdateVenueCommand>{
    public async Task<Result> Handle(UpdateVenueCommand request , CancellationToken cancellationToken ){
        var venue = await venueRepository.FindByIdAsync(request.Id,cancellationToken);
        if(venue is null){
            return Result.Failure(Error.NotFound("Venue.NotFound","A venue with this id does not exist."));
        }
        venue.UpdateInfo(request.Name, request.Address, request.Latitude, request.Longitude, request.OpenTime, request.CloseTime, request.Description);
        venueRepository.Update(venue);
        return Result.Success();
    }
}