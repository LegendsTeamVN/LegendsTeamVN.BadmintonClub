using LegendsTeamVN.Core.Domain.Events;
using MediatR;

namespace LegendsTeamVN.Core.Application.Messaging.Events;

public interface IDomainEventHandler<in TEvent> : INotificationHandler<TEvent> 
    where TEvent : IDomainEvent;
