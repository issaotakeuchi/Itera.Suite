using Itera.Suite.Domain.Enums;
using Itera.Suite.Domain.ValueObjects;

namespace Itera.Suite.Domain.Entities;

public class ItemDeCusto
{
    public Guid Id { get; set; }
    public Guid GrupoDeEstudoId { get; set; }
    public string Categoria { get; set; }         // Ex: "Transporte", "Hospedagem", "Guia"
    public string Descricao { get; set; }         // Ex: "Ônibus Transcomin", "Pousada Cavernas"
    public Guid? FornecedorId { get; set; }
    public Fornecedor? Fornecedor { get; set; }        // Nome da empresa ou pessoa prestadora
    public int Diarias { get; set; }              // Ex: 3
    public int Quantidade { get; set; }           // Ex: 24
    public decimal ValorUnitario { get; set; }    // Ex: 2.62
    public decimal Total => Diarias * Quantidade * ValorUnitario;
    public StatusProjeto Status { get; set; }            // Ex: "Orçado", "Confirmado", "Pago", "Cancelado"
    public ICollection<ObservacaoItem> Observacoes { get; set; } = new List<ObservacaoItem>();
    public ICollection<PagamentoProgramado> Pagamentos { get; set; } = new List<PagamentoProgramado>();
}
