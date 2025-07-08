using Itera.Suite.Domain.Entities;

namespace Itera.Suite.Shared.ValueObjects;

public class ObservacaoProjeto : ObservacaoBase
{
    public Guid ProjetoDeViagemId { get; set; }
    public ProjetoDeViagem ProjetoDeViagem { get; set; }
}

