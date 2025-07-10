namespace Itera.Suite.Domain.Entities;

public class QuitacaoDaOrdemDePagamento
{
    public Guid Id { get; set; }
    public Guid OrdemDePagamentoId { get; set; }
    public OrdemDePagamento OrdemDePagamento { get; set; }
    public Guid PagamentoDaOrdemDePagamentoId { get; set; }
    public PagamentoDaOrdemDePagamento Pagamento { get; set; }
    public decimal ValorQuitado { get; set; }
    public DateTime DataQuitacao { get; set; }
    public ICollection<ObservacaoDaQuitacaoDaOrdemDePagamento> Observacoes { get; set; } = new List<ObservacaoDaQuitacaoDaOrdemDePagamento>();
}