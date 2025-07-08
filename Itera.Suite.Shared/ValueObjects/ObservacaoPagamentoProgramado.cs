using Itera.Suite.Domain.Entities;
using Itera.Suite.Shared.ValueObjects;

namespace Itera.Suite.Shared.ValueObjects;

public class ObservacaoPagamentoProgramado : ObservacaoBase
{
    public Guid PagamentoProgramadoId { get; set; }
    public PagamentoProgramado PagamentoProgramado { get; set; }
}

