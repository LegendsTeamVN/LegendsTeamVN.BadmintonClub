using LegendsTeamVN.BadmintonClub.Application.DTOs.Venues.Responses;
using LegendsTeamVN.BadmintonClub.Application.DTOs.Venues.Requests;
using LegendsTeamVN.Core.Application.Messaging.CQRS;
using LegendsTeamVN.Core.Utilities.Pagination;

namespace LegendsTeamVN.BadmintonClub.Application.Features.Venues.GetList;

public sealed record GetVenueQuery(
    GetVenuesRequest Filter
) : IQuery<PagedResult<VenueResponse>>;