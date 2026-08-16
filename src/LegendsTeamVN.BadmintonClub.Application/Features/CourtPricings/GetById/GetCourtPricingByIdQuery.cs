using LegendsTeamVN.BadmintonClub.Application.DTOs.CourtPricings.Responses;
using LegendsTeamVN.Core.Application.Messaging.CQRS;

namespace LegendsTeamVN.BadmintonClub.Application.Features.CourtPricings.GetById;

public sealed record GetCourtPricingByIdQuery(Guid Id) : IQuery<CourtPricingResponse>;
