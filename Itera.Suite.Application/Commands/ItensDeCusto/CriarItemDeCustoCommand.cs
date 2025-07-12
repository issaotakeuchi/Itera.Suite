namespace Itera.Suite.Application.Commands.ItensDeCusto;

public class CriarItemDeCustoCommand
{
    public Guid ProjetoDeViagemId { get; set; }
    public string Categoria { get; set; } = null!;
    public string? Descricao { get; set; } = null!;
    public int Diarias { get; set; }
    public int Quantidade { get; set; }
    public decimal ValorUnitario { get; set; }
    public Guid? FornecedorId { get; set; }
}
