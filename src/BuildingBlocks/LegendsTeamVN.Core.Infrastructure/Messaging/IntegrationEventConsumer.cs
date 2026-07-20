using LegendsTeamVN.Core.Application.Messaging.Events;
using MassTransit;

namespace LegendsTeamVN.Core.Infrastructure.Messaging;

public sealed class IntegrationEventConsumer<TIntegrationEvent>(
    IIntegrationEventHandler<TIntegrationEvent> handler) : IConsumer<TIntegrationEvent>
    where TIntegrationEvent : class, IIntegrationEvent
{
    private readonly IIntegrationEventHandler<TIntegrationEvent> _handler = handler;

    public Task Consume(ConsumeContext<TIntegrationEvent> context)
    {
        return _handler.Handle(context.Message, context.CancellationToken);
    }
}
