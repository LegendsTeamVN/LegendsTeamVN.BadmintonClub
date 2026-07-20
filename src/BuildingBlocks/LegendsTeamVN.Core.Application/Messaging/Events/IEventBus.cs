namespace LegendsTeamVN.Core.Application.Messaging.Events;

public interface IEventBus
{
    Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default)
        where T : IIntegrationEvent;
}
