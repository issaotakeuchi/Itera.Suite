using Itera.Suite.Shared.Enums;

namespace Itera.Suite.Application.DTOs;

public class ClienteDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? ContatoPrincipal { get; set; }
    public string? Documento { get; set; } // CPF/CNPJ
    public string? Telefone { get; set; }
    public string? Tipo { get; set; } // Enum em vez de string
    public List<ProjetoResumoDto> Projetos { get; set; } = new();
}
