namespace Itera.Suite.Front.Services.Cliente;

public class ClienteModel
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string ContatoPrincipal { get; set; }
    public string Documento { get; set; }
    public string Telefone { get; set; }

    public List<ProjetoDto> Projetos { get; set; } = new(); // se tiver projetos!
}

public class ProjetoDto
{
    public Guid Id { get; set; }
    public string NomeInterno { get; set; }
}