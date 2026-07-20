using LegendsTeamVN.Core.Application.Messaging.Events;
using MassTransit;

namespace LegendsTeamVN.Core.Infrastructure.Messaging;

public sealed class EventBus(IPublishEndpoint publishEndpoint) : IEventBus
{
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

    public Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default) 
        where T : IIntegrationEvent
    {
        return _publishEndpoint.Publish(@event, cancellationToken);
    }
}
