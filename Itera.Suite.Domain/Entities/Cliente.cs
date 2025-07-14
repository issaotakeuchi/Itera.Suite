using Itera.Suite.Domain.Common;
using Itera.Suite.Domain.Entities;
using Itera.Suite.Shared.Enums;

public class Cliente : AuditableEntity
{
    public string Nome { get; private set; }
    public string? Documento { get; private set; } // CPF/CNPJ
    public TipoCliente? Tipo { get; private set; } // Enum em vez de string
    public string? ContatoPrincipal { get; private set; }
    public string? Email { get; private set; }
    public string? Telefone { get; private set; }

    private readonly List<ProjetoDeViagem> _projetos = new();
    public IReadOnlyCollection<ProjetoDeViagem> Projetos => _projetos.AsReadOnly();

    protected Cliente() { } // EF Core

    public Cliente(
        string nome,
        string? documento,
        TipoCliente? tipo,
        string? contatoPrincipal,
        string? email,
        string? telefone,
        string criadoPor)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome não pode ser vazio.");

        Nome = nome;
        Documento = documento;
        Tipo = tipo;
        ContatoPrincipal = contatoPrincipal;
        Email = email;
        Telefone = telefone;

        CriadoPor = criadoPor;
        DataCriacao = DateTime.UtcNow;
    }

    public void AtualizarDados(
    string? nome,
    string? documento,
    string? tipo,
    string? contatoPrincipal,
    string? email,
    string? telefone,
    string atualizadoPor)
    {
        Nome = nome ?? Nome;
        Documento = documento ?? Documento;
        if (!string.IsNullOrWhiteSpace(tipo))
            Tipo = Enum.Parse<TipoCliente>(tipo, true);
        ContatoPrincipal = contatoPrincipal ?? ContatoPrincipal;
        Email = email ?? Email;
        Telefone = telefone ?? Telefone;

        AtualizadoPor = atualizadoPor;
        DataAtualizacao = DateTime.UtcNow;
    }

    public void Inativar(string atualizadoPor)
    {
        AtualizadoPor = atualizadoPor;
        DataAtualizacao = DateTime.UtcNow;
        // Se quiser, cria um campo IsAtivo:
        // IsAtivo = false;
    }

}
