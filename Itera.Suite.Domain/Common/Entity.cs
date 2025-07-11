namespace Itera.Suite.Domain.Common;

public abstract class Entity
{
    public Guid Id { get; protected set; } = Guid.NewGuid();

    // Opcional: Equals/GetHashCode para comparar Entities por Id
    public override bool Equals(object obj)
    {
        if (obj is not Entity other) return false;
        if (ReferenceEquals(this, other)) return true;
        if (GetType() != other.GetType()) return false;
        return Id.Equals(other.Id);
    }

    public override int GetHashCode() => Id.GetHashCode();
}
