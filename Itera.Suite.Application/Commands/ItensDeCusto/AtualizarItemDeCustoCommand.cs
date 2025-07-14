namespace Itera.Suite.Application.Commands.ItensDeCusto;

public class AtualizarItemDeCustoCommand
{
    public Guid Id { get; set; }
    public string? Categoria { get; set; }
    public string? Descricao { get; set; }
    public int? Diarias { get; set; }
    public int? Quantidade { get; set; }
    public decimal? ValorUnitario { get; set; }
    public Guid? FornecedorId { get; set; }
}
