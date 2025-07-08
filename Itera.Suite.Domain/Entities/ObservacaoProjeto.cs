using Itera.Suite.Domain.ValueObjects;

namespace Itera.Suite.Domain.Entities;

public class ObservacaoProjeto : ObservacaoBase
{
    public Guid ProjetoDeViagemId { get; set; }
    public ProjetoDeViagem ProjetoDeViagem { get; set; }
}

