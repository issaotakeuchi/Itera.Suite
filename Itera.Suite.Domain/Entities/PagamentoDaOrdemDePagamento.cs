using Itera.Suite.Shared.Enums;

namespace Itera.Suite.Domain.Entities;

public class PagamentoDaOrdemDePagamento
{
    public Guid Id { get; set; }
    public decimal ValorTotal { get; set; }
    public DateTime DataPagamento { get; set; }
    public FormaPagamento FormaPagamento { get; set; }
    public ComprovanteDaOrdemDePagamento ComprovanteDaOrdemDePagamento { get; set; }
    public List<QuitacaoDaOrdemDePagamento> Quitacoes { get; set; } = new();
}
