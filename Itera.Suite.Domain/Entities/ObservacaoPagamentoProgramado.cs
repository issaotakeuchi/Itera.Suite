using Itera.Suite.Domain.ValueObjects;

namespace Itera.Suite.Domain.Entities;

public class ObservacaoPagamentoProgramado : ObservacaoBase
{
    public Guid PagamentoProgramadoId { get; set; }
    public PagamentoProgramado PagamentoProgramado { get; set; }
}

