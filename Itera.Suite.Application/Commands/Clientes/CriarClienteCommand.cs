namespace Itera.Suite.Application.Commands.Clientes;

public class CriarClienteCommand
{
    public string Nome { get; set; } = null!;
    public string? Documento { get; set; }
    public string? Tipo { get; set; } // PessoaFisica ou PessoaJuridica
    public string? ContatoPrincipal { get; set; }
    public string? Email { get; set; }
    public string? Telefone { get; set; }
}
