namespace WeatherServices.SharedKernel.Core.Domain;

public abstract class Entity
{
    public int Id { get; protected init; }
    public Guid Uuid { get; protected init; }
    public DateTime CreatedAt { get; protected init; }
    public DateTime? UpdatedAt { get; protected set; }  

    public override bool Equals(object? obj)
    {
        if (obj is not Entity other)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        if (Id.Equals(0) || other.Id.Equals(0) && Uuid.Equals(Guid.Empty) && other.Uuid.Equals(Guid.Empty))
            return false;

        return Id.Equals(other.Id) && Uuid.Equals(other.Uuid);
    }
    public static bool operator ==(Entity? left, Entity? right)
    {
        if (left is null && right is null)
            return true;

        if (left is null || right is null)
            return false;

        return left.Equals(right);
    }
    public static bool operator !=(Entity left, Entity right)
    {
        return !(left == right);
    }
    public override int GetHashCode()
    {
        unchecked // Allow overflow, the default behavior of C#
        {
            int hash = 17;
            hash = hash * 41 + Uuid.GetHashCode();
            hash = hash * 41 + Id.GetHashCode();
            return hash;
        }
    }
}