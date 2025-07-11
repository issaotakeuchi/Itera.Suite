using Itera.Suite.Domain.Common;

namespace Itera.Suite.Domain.Entities;

public class ObservacaoProjeto : ObservacaoBase
{
    public Guid ProjetoDeViagemId { get; private set; }
    public ProjetoDeViagem ProjetoDeViagem { get; private set; }

    protected ObservacaoProjeto() { } // EF Core

    public ObservacaoProjeto(string texto, Guid autorId)
        : base(texto, autorId)
    {
    }
}
