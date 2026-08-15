using LegendsTeamVN.BadmintonClub.Application.DTOs.VenueSchedules.Responses;
using LegendsTeamVN.Core.Application.Messaging.CQRS;

namespace LegendsTeamVN.BadmintonClub.Application.Features.VenueSchedules.GetById;

public sealed record GetVenueScheduleByIdQuery(Guid Id) : IQuery<VenueScheduleResponse>;
