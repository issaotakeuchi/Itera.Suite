using Itera.Suite.Domain.Entities;

namespace Itera.Suite.Domain.ValueObjects;

public class ObservacaoPagamento : ObservacaoBase
{
    public Guid PagamentoProgramadoId { get; set; }
    public PagamentoProgramado PagamentoProgramado { get; set; }
}

