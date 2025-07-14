namespace Itera.Suite.Application.Commands.Clientes;

public class AtualizarClienteCommand
{
    public Guid Id { get; set; }
    public string? Nome { get; set; }
    public string? Documento { get; set; }
    public string? Tipo { get; set; }
    public string? ContatoPrincipal { get; set; }
    public string? Email { get; set; }
    public string? Telefone { get; set; }
}
