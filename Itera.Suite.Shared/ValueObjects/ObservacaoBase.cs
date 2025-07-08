namespace Itera.Suite.Shared.ValueObjects;

public abstract class ObservacaoBase
{
    public Guid Id { get; set; }
    public string Texto { get; set; }
    public DateTime DataHora { get; set; }
    public string Autor { get; set; }
    protected ObservacaoBase()
    {
        DataHora = DateTime.UtcNow;
    }
}

