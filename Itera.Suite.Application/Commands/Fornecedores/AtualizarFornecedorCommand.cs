namespace Itera.Suite.Application.Commands.Fornecedores;

public class AtualizarFornecedorCommand
{
    public Guid Id { get; set; }
    public string? Nome { get; set; }
    public string? Contato { get; set; }
    public string? Email { get; set; }
    public string? Telefone { get; set; }
    public string? TipoDeServico { get; set; }
}
