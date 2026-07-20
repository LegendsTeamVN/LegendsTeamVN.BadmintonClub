namespace LegendsTeamVN.Core.Application.Messaging.Events;

public interface IIntegrationEvent
{
    Guid Id { get; init; }
    DateTime OccurredOnUtc { get; init; }
}
