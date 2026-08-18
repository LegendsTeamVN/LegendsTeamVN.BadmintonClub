using LegendsTeamVN.BadmintonClub.Application.DTOs.Venues.Responses;
using LegendsTeamVN.Core.Application.Messaging.CQRS;

namespace LegendsTeamVN.BadmintonClub.Application.Features.Venues.GetById;

public sealed record GetVenueByIdQuery(
    Guid Id
) : IQuery<VenueResponse>;