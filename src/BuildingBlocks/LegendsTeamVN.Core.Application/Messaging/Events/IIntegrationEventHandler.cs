namespace LegendsTeamVN.Core.Application.Messaging.Events;

public interface IIntegrationEventHandler<in TEvent> where TEvent : IIntegrationEvent
{
    Task Handle(TEvent @event, CancellationToken cancellationToken = default);
}
