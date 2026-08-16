using LegendsTeamVN.BadmintonClub.Application.DTOs.CourtPricings.Requests;
using LegendsTeamVN.BadmintonClub.Application.DTOs.CourtPricings.Responses;
using LegendsTeamVN.Core.Application.Messaging.CQRS;
using LegendsTeamVN.Core.Utilities.Pagination;

namespace LegendsTeamVN.BadmintonClub.Application.Features.CourtPricings.GetList;

public sealed record GetCourtPricingsQuery(GetCourtPricingsRequest Filter) : IQuery<PagedResult<CourtPricingResponse>>;
