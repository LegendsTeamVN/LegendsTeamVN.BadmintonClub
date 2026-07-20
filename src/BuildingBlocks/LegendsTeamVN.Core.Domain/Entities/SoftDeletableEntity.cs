namespace LegendsTeamVN.Core.Domain.Entities;

public abstract class SoftDeletableEntity<T> : AuditableEntity<T>, ISoftDeletable
{
    public bool IsDeleted { get; protected set; }
    public DateTimeOffset? DeletedOnUtc { get; protected set; }
    public string? DeletedBy { get; protected set; }
}
