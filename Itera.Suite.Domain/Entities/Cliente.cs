using Itera.Suite.Domain.Common;
using Itera.Suite.Domain.Entities;
using Itera.Suite.Shared.Enums;

public class Cliente : Entity
{
    public string Nome { get; private set; }
    public string Documento { get; private set; } // CPF/CNPJ
    public TipoCliente Tipo { get; private set; } // Enum em vez de string
    public string ContatoPrincipal { get; private set; }
    public string Email { get; private set; }
    public string Telefone { get; private set; }

    private readonly List<ProjetoDeViagem> _projetos = new();
    public IReadOnlyCollection<ProjetoDeViagem> Projetos => _projetos.AsReadOnly();

    protected Cliente() { } // EF Core

    public Cliente(string nome, string documento, TipoCliente tipo, string contatoPrincipal, string email, string telefone)
    {
        if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("Nome não pode ser vazio.");
        if (string.IsNullOrWhiteSpace(documento)) throw new ArgumentException("Documento não pode ser vazio.");
        if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email não pode ser vazio.");
        if (string.IsNullOrWhiteSpace(telefone)) throw new ArgumentException("Telefone não pode ser vazio.");

        Nome = nome;
        Documento = documento;
        Tipo = tipo;
        ContatoPrincipal = contatoPrincipal;
        Email = email;
        Telefone = telefone;
    }
}
