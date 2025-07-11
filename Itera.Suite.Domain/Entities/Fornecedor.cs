using Itera.Suite.Domain.Common;
using Itera.Suite.Domain.Entities;
using Itera.Suite.Shared.Enums;

public class Fornecedor : Entity
{
    public string Nome { get; private set; }
    public string? Contato { get; private set; }
    public string? Email { get; private set; }
    public string? Telefone { get; private set; }
    public TipoServico? TipoDeServico { get; private set; }
    public bool Ativo { get; private set; } = true;
    public string CriadoPor { get; private set; }
    public DateTime DataCriacao { get; private set; }
    public string? AtualizadoPor { get; private set; }
    public DateTime? DataAtualizacao { get; private set; }
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
        CriadoPor = criadoPor;
        Contato = contato;
        Email = email;
        Telefone = telefone;
        TipoDeServico = tipoDeServico;

        Ativo = true;
        DataCriacao = DateTime.UtcNow;
    }

    public void Inativar(string atualizadoPor)
    {
        Ativo = false;
        AtualizadoPor = atualizadoPor;
        DataAtualizacao = DateTime.UtcNow;
    }

    public void AtualizarContato(string? contato, string? email, string? telefone, string atualizadoPor)
    {
        Contato = contato;
        Email = email;
        Telefone = telefone;
        AtualizadoPor = atualizadoPor;
        DataAtualizacao = DateTime.UtcNow;
    }
}
