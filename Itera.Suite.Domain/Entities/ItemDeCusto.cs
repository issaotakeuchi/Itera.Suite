using Itera.Suite.Domain.Common;
using Itera.Suite.Shared.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Itera.Suite.Domain.Entities;

public class ItemDeCusto : AuditableEntity
{
    public Guid ProjetoDeViagemId { get; private set; }
    public ProjetoDeViagem ProjetoDeViagem { get; private set; }
    public CategoriaItemDeCusto Categoria { get; private set; }        // Ex: "Transporte", "Hospedagem", "Guia"
    public string? Descricao { get; private set; }         // Ex: "Ônibus Transcomin", "Pousada Cavernas"
    public Guid? FornecedorId { get; private set; }
    public Fornecedor? Fornecedor { get; private set; }        // Nome da empresa ou pessoa prestadora
    public int Diarias { get; private set; }             // Ex: 3
    public int Quantidade { get; private set; }           // Ex: 24
    public decimal ValorUnitario { get; private set; }    // Ex: 2.62
    
    [NotMapped]
    public decimal Total => Diarias * Quantidade * ValorUnitario;

    private readonly List<ObservacaoItem> _observacoes = new();
    public IReadOnlyCollection<ObservacaoItem> Observacoes => _observacoes.AsReadOnly();

    private readonly List<OrdemDePagamento> _pagamentos = new();
    public IReadOnlyCollection<OrdemDePagamento> Pagamentos => _pagamentos.AsReadOnly();

    public StatusItemDeCusto StatusAtual { get; private set; } = StatusItemDeCusto.Rascunho;

    private readonly List<RegistroStatusItemDeCusto> _historicoStatus = new();
    public IReadOnlyCollection<RegistroStatusItemDeCusto> HistoricoStatus => _historicoStatus.AsReadOnly();

    // ⚙️ Construtor vazio para EF Core
    #pragma warning disable CS8618
    protected ItemDeCusto() { }
    #pragma warning restore CS8618

    // ⚙️ Construtor rico
    public ItemDeCusto(
        Guid projetoDeViagemId,
        CategoriaItemDeCusto categoria,
        string? descricao,
        int diarias,
        int quantidade,
        decimal valorUnitario,
        Guid? fornecedorId,
        string criadoPor)
    {
        if (projetoDeViagemId == Guid.Empty)
            throw new ArgumentException("ProjetoDeViagemId não pode ser vazio.");

        if (diarias <= 0)
            throw new ArgumentException("Diárias deve ser maior que zero.");

        if (quantidade <= 0)
            throw new ArgumentException("Quantidade deve ser maior que zero.");

        if (valorUnitario <= 0)
            throw new ArgumentException("Valor unitário deve ser maior que zero.");

        ProjetoDeViagemId = projetoDeViagemId;
        Categoria = categoria;
        Descricao = descricao;
        Diarias = diarias;
        Quantidade = quantidade;
        ValorUnitario = valorUnitario;
        FornecedorId = fornecedorId;
        CriadoPor = criadoPor;
        DataCriacao = DateTime.UtcNow;
        StatusAtual = StatusItemDeCusto.Rascunho;

        // Opcional: inicializa primeiro histórico
        _historicoStatus.Add(new RegistroStatusItemDeCusto
        {
            Status = StatusItemDeCusto.Rascunho,
            DataHora = DateTime.UtcNow,
            UsuarioResponsavel = "Sistema",
            Justificativa = "Item criado em rascunho"
        });
    }

    // ⚙️ Método de negócio para alterar status (já existente)
    public void AlterarStatus(StatusItemDeCusto novoStatus, string usuario, string? justificativa = null)
    {
        if (StatusAtual != novoStatus)
        {
            StatusAtual = novoStatus;
            _historicoStatus.Add(new RegistroStatusItemDeCusto
            {
                Status = novoStatus,
                DataHora = DateTime.UtcNow,
                UsuarioResponsavel = usuario,
                Justificativa = justificativa
            });
        }
    }

    public decimal CalcularTotal() => Diarias * Quantidade * ValorUnitario;

    public void AtualizarDados(
    CategoriaItemDeCusto? categoria,
    string? descricao,
    int? diarias,
    int? quantidade,
    decimal? valorUnitario,
    Guid? fornecedorId,
    string atualizadoPor)
    {
        if (categoria.HasValue) Categoria = categoria.Value;
        Descricao = descricao ?? Descricao;
        Diarias = diarias ?? Diarias;
        Quantidade = quantidade ?? Quantidade;
        ValorUnitario = valorUnitario ?? ValorUnitario;
        FornecedorId = fornecedorId;

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
