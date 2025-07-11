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

    protected Fornecedor() { } // EF

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
}
