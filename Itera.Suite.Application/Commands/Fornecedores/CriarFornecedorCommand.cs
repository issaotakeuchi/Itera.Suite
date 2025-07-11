namespace Itera.Suite.Application.Commands.Fornecedores;

public class CriarFornecedorCommand
{
    public string Nome { get; set; } = null!;
    public string? Contato { get; set; }
    public string? Email { get; set; }
    public string? Telefone { get; set; }
    public string? TipoDeServico { get; set; } // Enum: Transporte, Hospedagem, etc.
}
