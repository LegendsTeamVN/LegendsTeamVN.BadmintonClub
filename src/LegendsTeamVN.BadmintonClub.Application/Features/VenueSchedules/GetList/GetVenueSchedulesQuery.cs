using LegendsTeamVN.BadmintonClub.Application.DTOs.VenueSchedules.Requests;
using LegendsTeamVN.BadmintonClub.Application.DTOs.VenueSchedules.Responses;
using LegendsTeamVN.Core.Application.Messaging.CQRS;
using LegendsTeamVN.Core.Utilities.Pagination;

namespace LegendsTeamVN.BadmintonClub.Application.Features.VenueSchedules.GetList;

public sealed record GetVenueSchedulesQuery(GetVenueSchedulesRequest Filter) : IQuery<PagedResult<VenueScheduleResponse>>;
