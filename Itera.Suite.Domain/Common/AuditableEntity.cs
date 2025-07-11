namespace Itera.Suite.Domain.Common;

public abstract class AuditableEntity : Entity
{
    public string CriadoPor { get; protected set; } = "Sistema";
    public DateTime DataCriacao { get; protected set; } = DateTime.UtcNow;

    public string? AtualizadoPor { get; protected set; }
    public DateTime? DataAtualizacao { get; protected set; }
}
