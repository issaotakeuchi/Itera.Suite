namespace Itera.Suite.Domain.Common;

public abstract class ObservacaoBase : Entity
{
    public string Texto { get; protected set; }
    public Guid AutorId { get; protected set; }
    public DateTime DataCriacao { get; protected set; }
    public DateTime? DataUltimaAtualizacao { get; protected set; }

    protected ObservacaoBase() { } // EF

    protected ObservacaoBase(string texto, Guid autorId)
    {
        if (string.IsNullOrWhiteSpace(texto))
            throw new ArgumentException("Texto não pode ser vazio.");

        Texto = texto;
        AutorId = autorId;
        DataCriacao = DateTime.UtcNow;
    }

    public void EditarTexto(string novoTexto)
    {
        if (string.IsNullOrWhiteSpace(novoTexto))
            throw new ArgumentException("Texto não pode ser vazio.");

        Texto = novoTexto;
        DataUltimaAtualizacao = DateTime.UtcNow;
    }
}
