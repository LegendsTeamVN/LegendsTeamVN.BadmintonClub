using LegendsTeamVN.BadmintonClub.Application.DTOs.Courts.Responses;
using LegendsTeamVN.Core.Application.Messaging.CQRS;
using LegendsTeamVN.Core.Utilities.Results;

namespace LegendsTeamVN.BadmintonClub.Application.Features.Courts.GetById;

public sealed record GetCourtByIdQuery(Guid Id) : IQuery<CourtResponse>;
