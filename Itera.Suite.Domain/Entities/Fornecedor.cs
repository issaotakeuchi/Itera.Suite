using Itera.Suite.Domain.Common;
using Itera.Suite.Domain.Entities;
using Itera.Suite.Shared.Enums;

public class Fornecedor : AuditableEntity
{
    public string Nome { get; private set; }
    public string? Contato { get; private set; }
    public string? Email { get; private set; }
    public string? Telefone { get; private set; }
    public TipoServico? TipoDeServico { get; private set; }
    public bool Ativo { get; private set; } = true;
    private readonly List<ItemDeCusto> _itensDeCusto = new();
    public IReadOnlyCollection<ItemDeCusto> ItensDeCusto => _itensDeCusto.AsReadOnly();

    // Construtor para EF Core
    #pragma warning disable CS8618
    protected Fornecedor() { }
    #pragma warning restore CS8618

    public Fornecedor(
        string nome,
        string criadoPor,
        string? contato = null,
        string? email = null,
        string? telefone = null,
        TipoServico? tipoDeServico = null)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome não pode ser vazio.");
        if (string.IsNullOrWhiteSpace(criadoPor))
            throw new ArgumentException("CriadoPor não pode ser vazio.");

        Nome = nome;
        Contato = contato;
        Email = email;
        Telefone = telefone;
        TipoDeServico = tipoDeServico;

        CriadoPor = criadoPor;
        DataCriacao = DateTime.UtcNow;
    }

    public void AtualizarDados(string? nome, string? contato, string? email, string? telefone, TipoServico? tipo, string atualizadoPor)
    {
        if (!string.IsNullOrWhiteSpace(nome)) Nome = nome;
        Contato = contato ?? Contato;
        Email = email ?? Email;
        Telefone = telefone ?? Telefone;
        TipoDeServico = tipo;

        AtualizadoPor = atualizadoPor;
        DataAtualizacao = DateTime.UtcNow;
    }

    public void Inativar(string atualizadoPor)
    {
        AtualizadoPor = atualizadoPor;
        DataAtualizacao = DateTime.UtcNow;
        // Se quiser: IsAtivo = false;
    }
}
