using Itera.Suite.Domain.Enums;
using Itera.Suite.Shared.Enums;

namespace Itera.Suite.Domain.Entities;

public class PagamentoProgramado
{
    public Guid Id { get; set; }
    public Guid ItemDeCustoId { get; set; }
    public ItemDeCusto ItemDeCusto { get; set; }
    public DateTime DataPrevista { get; set; }
    public decimal Valor { get; set; }
    public FormaPagamento Forma { get; set; }
    public ICollection<ObservacaoPagamentoProgramado> Observacoes { get; set; } = new List<ObservacaoPagamentoProgramado>();
    public StatusPagamentoProgramado Status { get; set; }
    public string? ComprovanteUrl { get; set; } // Ou path local, caso não use storage externo ainda
}
