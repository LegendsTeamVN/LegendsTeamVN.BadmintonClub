using LegendsTeamVN.Core.Domain.Entities;
using LegendsTeamVN.Core.Domain.Events;

namespace LegendsTeamVN.Core.Domain.Aggregates;

public abstract class AggregateRoot<T> : SoftDeletableEntity<T>, IAggregateRoot
{
    private readonly List<IDomainEvent> _domainEvents = [];

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void RaiseDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);

    public void ClearDomainEvents() => _domainEvents.Clear();
}

