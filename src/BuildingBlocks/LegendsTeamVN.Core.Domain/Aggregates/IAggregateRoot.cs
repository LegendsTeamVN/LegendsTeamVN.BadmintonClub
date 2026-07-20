using LegendsTeamVN.Core.Domain.Events;

namespace LegendsTeamVN.Core.Domain.Aggregates;

public interface IAggregateRoot
{
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }

    void ClearDomainEvents();
}
