using Itera.Suite.Domain.Enums;
using Itera.Suite.Shared.Enums;

namespace Itera.Suite.Domain.Entities;

public class OrdemDePagamento
{
    public Guid Id { get; set; }
    public Guid ItemDeCustoId { get; set; }
    public ItemDeCusto ItemDeCusto { get; set; }
    public decimal ValorAutorizado { get; set; }
    public DateTime DataPrevista { get; set; }
    public FormaPagamento Forma { get; set; }
    public StatusOrdemDePagamento Status { get; set; }
    public ICollection<ObservacaoDaOrdemDePagamento> Observacoes { get; set; } = new List<ObservacaoDaOrdemDePagamento>();
    public List<QuitacaoDaOrdemDePagamento> Quitacoes { get; set; } = new();
}
