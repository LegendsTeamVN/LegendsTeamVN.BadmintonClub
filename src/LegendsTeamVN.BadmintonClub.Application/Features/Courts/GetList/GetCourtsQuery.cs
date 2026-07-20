using LegendsTeamVN.BadmintonClub.Application.DTOs.Courts.Requests;
using LegendsTeamVN.BadmintonClub.Application.DTOs.Courts.Responses;
using LegendsTeamVN.Core.Application.Messaging.CQRS;
using LegendsTeamVN.Core.Utilities.Pagination;

namespace LegendsTeamVN.BadmintonClub.Application.Features.Courts.GetList;

public sealed record GetCourtsQuery(GetCourtsRequest Filter) : IQuery<PagedResult<CourtResponse>>;
