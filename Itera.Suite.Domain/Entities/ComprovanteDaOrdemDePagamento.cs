namespace Itera.Suite.Domain.Entities;

public class ComprovanteDaOrdemDePagamento
{
    public Guid Id { get; set; }
    public Guid PagamentoDaOrdemDePagamentoId { get; set; }
    public PagamentoDaOrdemDePagamento Pagamento { get; set; }
    public string Url { get; set; }
    public string NomeArquivoOriginal { get; set; }
    public DateTime DataUpload { get; set; }
    public string AnexadoPor { get; set; }
}
