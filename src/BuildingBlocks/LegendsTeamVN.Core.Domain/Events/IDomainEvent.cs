using MediatR;

namespace LegendsTeamVN.Core.Domain.Events;

public interface IDomainEvent : INotification
{
    Guid Id { get; init; }
}
