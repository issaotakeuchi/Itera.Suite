using Itera.Suite.Shared.Enums;

namespace Itera.Suite.Application.Commands.ItensDeCusto;

public class CriarItemDeCustoCommand
{
    public Guid ProjetoDeViagemId { get; set; }
    public string Categoria { get; set; } = null!;
    public string? Descricao { get; set; } = null!;
    public int Diarias { get; set; }
    public int Quantidade { get; set; }
    public decimal ValorPadrao { get; set; }
    public TipoCalculoItem TipoCalculo { get; set; }
    public Guid? FornecedorId { get; set; }
}
