namespace Itera.Suite.Application.DTOs;

public class ItemDeCustoDto
{
    public Guid Id { get; set; }
    public string Categoria { get; set; } = null!;
    public string? Descricao { get; set; }
    public int Diarias { get; set; }
    public int Quantidade { get; set; }
    public decimal ValorUnitario { get; set; }
    public decimal Total { get; set; }
    public string StatusAtual { get; set; } = null!;
    public string? FornecedorNome { get; set; }
    public string ProjetoNome { get; set; } = null!;
    public decimal ValorTotal { get; set; }  // 👈 ADICIONA ISSO!
    public string Status { get; set; } = null!;
}
