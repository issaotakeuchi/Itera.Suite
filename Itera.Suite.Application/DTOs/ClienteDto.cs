namespace Itera.Suite.Application.DTOs;

public class ClienteDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = null!;
    public List<ProjetoResumoDto> Projetos { get; set; } = new();
}
