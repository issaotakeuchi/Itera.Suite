using Itera.Suite.Domain.ValueObjects;

namespace Itera.Suite.Domain.Entities;

public class ObservacaoDaOrdemDePagamento : ObservacaoBase
{
    public Guid OrdemDeProgramadoId { get; set; }
    public OrdemDePagamento PagamentoProgramado { get; set; }
}

