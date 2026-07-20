namespace LegendsTeamVN.Core.Domain.Entities;

public abstract class Entity<T> : IEquatable<Entity<T>>
{
    public virtual T Id { get; protected set; } = default!;

    public bool Equals(Entity<T>? other)
    {
        if (other is null)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        if (GetType() != other.GetType())
            return false;

        if (Id is null || Id.Equals(default(T)))
            return false;

        return Id.Equals(other.Id);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Entity<T>);
    }

    public override int GetHashCode()
    {
        if (Id is null || Id.Equals(default(T)))
            return base.GetHashCode();

        return Id.GetHashCode();
    }

    public static bool operator ==(Entity<T>? left, Entity<T>? right)
    {
        if (left is null && right is null)
            return true;

        if (left is null || right is null)
            return false;

        return left.Equals(right);
    }

    public static bool operator !=(Entity<T>? left, Entity<T>? right)
    {
        return !(left == right);
    }
}
