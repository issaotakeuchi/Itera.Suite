namespace Itera.Suite.Domain.Entities;

public class Cliente
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Documento { get; set; } // CPF ou CNPJ
    public string Tipo { get; set; } // Pessoa Física / Jurídica
    public string ContatoPrincipal { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public ICollection<ProjetoDeViagem> Projetos { get; set; } = new List<ProjetoDeViagem>();
}
