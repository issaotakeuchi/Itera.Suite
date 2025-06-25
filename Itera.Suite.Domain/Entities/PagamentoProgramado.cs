using Itera.Suite.Domain.Enums;
using Itera.Suite.Domain.ValueObjects;

namespace Itera.Suite.Domain.Entities;

public class PagamentoProgramado
{
    public Guid Id { get; set; }
    public Guid ItemDeCustoId { get; set; }
    public ItemDeCusto ItemDeCusto { get; set; }
    public DateTime DataPrevista { get; set; }
    public decimal Valor { get; set; }
    public FormaPagamento Forma { get; set; }
    public ICollection<ObservacaoPagamento> Observacoes { get; set; } = new List<ObservacaoPagamento>();
}
