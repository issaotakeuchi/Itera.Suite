using Itera.Suite.Domain.Common;
using Itera.Suite.Shared.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Itera.Suite.Domain.Entities;

public class ItemDeCusto : AuditableEntity
{
    public Guid ProjetoDeViagemId { get; private set; }
    public ProjetoDeViagem ProjetoDeViagem { get; private set; } = null!;

    public CategoriaItemDeCusto Categoria { get; private set; }       // Ex: Transporte, Hospedagem, etc.
    public string? Descricao { get; private set; }

    public Guid? FornecedorId { get; private set; }
    public Fornecedor? Fornecedor { get; private set; }

    public int Diarias { get; private set; }
    public int Quantidade { get; private set; }
    public decimal ValorPadrao { get; private set; }                  // Ex: 5.00

    public TipoCalculoItem TipoCalculo { get; private set; }

    [NotMapped]
    public decimal Total => Diarias * Quantidade * ValorPadrao;

    private readonly List<ObservacaoItem> _observacoes = new();
    public IReadOnlyCollection<ObservacaoItem> Observacoes => _observacoes.AsReadOnly();

    private readonly List<OrdemDePagamento> _pagamentos = new();
    public IReadOnlyCollection<OrdemDePagamento> Pagamentos => _pagamentos.AsReadOnly();

    public StatusItemDeCusto StatusAtual { get; private set; } = StatusItemDeCusto.Rascunho;

    private readonly List<RegistroStatusItemDeCusto> _historicoStatus = new();
    public IReadOnlyCollection<RegistroStatusItemDeCusto> HistoricoStatus => _historicoStatus.AsReadOnly();

    // EF Core
#pragma warning disable CS8618
    protected ItemDeCusto() { }
#pragma warning restore CS8618

    public ItemDeCusto(
        Guid projetoDeViagemId,
        CategoriaItemDeCusto categoria,
        string? descricao,
        int diarias,
        int quantidade,
        decimal valorPadrao,
        TipoCalculoItem tipoCalculo,
        Guid? fornecedorId,
        string criadoPor)
    {
        if (projetoDeViagemId == Guid.Empty)
            throw new ArgumentException("ProjetoDeViagemId não pode ser vazio.");

        if (diarias <= 0)
            throw new ArgumentException("Diárias deve ser maior que zero.");

        if (quantidade <= 0)
            throw new ArgumentException("Quantidade deve ser maior que zero.");

        if (valorPadrao < 0)
            throw new ArgumentException("Valor padrão não pode ser negativo.");

        ProjetoDeViagemId = projetoDeViagemId;
        Categoria = categoria;
        Descricao = descricao;
        Diarias = diarias;
        Quantidade = quantidade;
        ValorPadrao = valorPadrao;
        TipoCalculo = tipoCalculo;
        FornecedorId = fornecedorId;

        CriadoPor = criadoPor;
        DataCriacao = DateTime.UtcNow;
        StatusAtual = StatusItemDeCusto.Rascunho;

        _historicoStatus.Add(new RegistroStatusItemDeCusto
        {
            Status = StatusItemDeCusto.Rascunho,
            DataHora = DateTime.UtcNow,
            UsuarioResponsavel = criadoPor ?? "Sistema",
            Justificativa = "Item criado em rascunho"
        });
    }

    public decimal CalcularTotal() => Diarias * Quantidade * ValorPadrao;

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

    public void AtualizarDados(
        CategoriaItemDeCusto? categoria,
        string? descricao,
        int? diarias,
        int? quantidade,
        decimal? valorPadrao,
        TipoCalculoItem? tipoCalculo,
        Guid? fornecedorId,
        string atualizadoPor)
    {
        if (categoria.HasValue) Categoria = categoria.Value;
        if (!string.IsNullOrWhiteSpace(descricao)) Descricao = descricao;
        if (diarias.HasValue) Diarias = diarias.Value;
        if (quantidade.HasValue) Quantidade = quantidade.Value;
        if (valorPadrao.HasValue)
        {
            if (valorPadrao.Value < 0)
                throw new ArgumentException("Valor padrão não pode ser negativo.");
            ValorPadrao = valorPadrao.Value;
        }
        if (tipoCalculo.HasValue) TipoCalculo = tipoCalculo.Value;

        FornecedorId = fornecedorId;
        AtualizadoPor = atualizadoPor ?? "Sistema";
        DataAtualizacao = DateTime.UtcNow;
    }

    public void Inativar(string atualizadoPor)
    {
        AtualizadoPor = atualizadoPor;
        DataAtualizacao = DateTime.UtcNow;
    }
}
