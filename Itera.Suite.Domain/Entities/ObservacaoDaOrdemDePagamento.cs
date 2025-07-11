using Itera.Suite.Domain.Common;

namespace Itera.Suite.Domain.Entities;

public class ObservacaoDaOrdemDePagamento : ObservacaoBase
{
    public Guid OrdemDePagamentoId { get; set; }
    public OrdemDePagamento OrdemDePagamento { get; set; }
}

