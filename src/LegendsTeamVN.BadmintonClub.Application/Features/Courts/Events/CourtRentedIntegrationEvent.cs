using LegendsTeamVN.Core.Application.Messaging.Events;

namespace LegendsTeamVN.BadmintonClub.Application.Features.Courts.Events;

public class CourtRentedIntegrationEvent : IIntegrationEvent
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public DateTime OccurredOnUtc { get; init; } = DateTime.UtcNow;

    public Guid CourtId { get; set; }
    public string RenterName { get; set; } = string.Empty;
    public DateTime RentedAt { get; set; }
}
