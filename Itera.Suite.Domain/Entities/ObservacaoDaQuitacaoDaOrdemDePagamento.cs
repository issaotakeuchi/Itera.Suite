using Itera.Suite.Domain.Common;

namespace Itera.Suite.Domain.Entities;

public class ObservacaoDaQuitacaoDaOrdemDePagamento : ObservacaoBase
{
    public Guid QuitacaoDaOrdemDePagamentoId { get; set; }
    public QuitacaoDaOrdemDePagamento QuitacaoDaOrdemDePagamento { get; set; }
}
