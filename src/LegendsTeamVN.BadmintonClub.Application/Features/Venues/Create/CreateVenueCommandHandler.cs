using LegendsTeamVN.BadmintonClub.Domain.Entities;
using LegendsTeamVN.BadmintonClub.Domain.Repositories;
using LegendsTeamVN.Core.Application.Messaging.CQRS;
using LegendsTeamVN.Core.Utilities.Results;

namespace LegendsTeamVN.BadmintonClub.Application.Features.Venues.Create;

public sealed class CreateVenueCommandHandler(IVenueRepository venueRepository): ICommandHandler<CreateVenueCommand, Guid>{
    public async Task<Result<Guid>> Handle(CreateVenueCommand request , CancellationToken cancellationToken ){
        var existingVenue = await venueRepository.FindSingleAsync(c => c.Name == request.Name,cancellationToken);
        if(existingVenue is not null){
            return Result.Failure<Guid>(Error.Conflict("Venue.NameExists","A venue with this name already exists."));
        }
        var venue = new Venue(request.Name, request.Address, request.Latitude, request.Longitude, request.OpenTime, request.CloseTime, request.Description);
        venueRepository.Add(venue);
        return venue.Id;
    }
}