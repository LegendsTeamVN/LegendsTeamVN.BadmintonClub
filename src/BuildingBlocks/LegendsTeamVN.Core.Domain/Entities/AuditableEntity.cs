namespace LegendsTeamVN.Core.Domain.Entities;

public abstract class AuditableEntity<T> : Entity<T>, IAuditable
{
    public DateTimeOffset CreatedOnUtc { get; protected set; }
    public string? CreatedBy { get; protected set; }

    public DateTimeOffset? ModifiedOnUtc { get; protected set; }
    public string? ModifiedBy { get; protected set; }
}
