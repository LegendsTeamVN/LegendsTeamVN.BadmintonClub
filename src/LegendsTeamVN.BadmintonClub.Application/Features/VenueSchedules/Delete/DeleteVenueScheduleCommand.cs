using LegendsTeamVN.Core.Application.Messaging.CQRS;

namespace LegendsTeamVN.BadmintonClub.Application.Features.VenueSchedules.Delete;

public sealed record DeleteVenueScheduleCommand(Guid Id) : ICommand;
