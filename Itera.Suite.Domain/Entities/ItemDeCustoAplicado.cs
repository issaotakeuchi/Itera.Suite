using Itera.Suite.Domain.Common;

namespace Itera.Suite.Domain.Entities;

public class ItemDeCustoAplicado : AuditableEntity
{
    // Relação com a base
    public Guid BaseDeCalculoId { get; private set; }
    public BaseDeCalculo BaseDeCalculo { get; private set; } = null!;

    // Relação com o item de custo
    public Guid ItemDeCustoId { get; private set; }
    public ItemDeCusto ItemDeCusto { get; private set; } = null!;

    public string Subtipo { get; private set; } = string.Empty;

    public int QuantidadeTotal { get; private set; }
    public int QuantidadeCortesia { get; private set; }

    public decimal ValorPadrao { get; private set; }
    public decimal? ValorEditado { get; private set; }

    public bool ConsideraNoLucro { get; private set; }

    public decimal ValorFinal => ValorEditado ?? ValorPadrao;
    public decimal Total => (QuantidadeTotal - QuantidadeCortesia) * ValorFinal;

    protected ItemDeCustoAplicado() { }

    public ItemDeCustoAplicado(
        BaseDeCalculo baseDeCalculo,
        ItemDeCusto item,
        string subtipo,
        int quantidadeTotal,
        int quantidadeCortesia,
        decimal? valorEditado,
        bool consideraNoLucro,
        string criadoPor)
    {
        if (baseDeCalculo == null) throw new ArgumentNullException(nameof(baseDeCalculo));
        if (item == null) throw new ArgumentNullException(nameof(item));
        if (quantidadeTotal < 0) throw new ArgumentException("Qtd total inválida.");
        if (quantidadeCortesia < 0 || quantidadeCortesia > quantidadeTotal)
            throw new ArgumentException("Cortesia inválida.");
        if (valorEditado is < 0)
            throw new ArgumentException("Valor editado não pode ser negativo.");

        BaseDeCalculo = baseDeCalculo;
        BaseDeCalculoId = baseDeCalculo.Id;

        ItemDeCusto = item;
        ItemDeCustoId = item.Id;

        Subtipo = subtipo ?? string.Empty;
        QuantidadeTotal = quantidadeTotal;
        QuantidadeCortesia = quantidadeCortesia;

        ValorPadrao = item.ValorPadrao;
        ValorEditado = (valorEditado.HasValue && valorEditado != item.ValorPadrao)
            ? valorEditado
            : null;

        ConsideraNoLucro = consideraNoLucro;

        CriadoPor = criadoPor ?? "Sistema";
        DataCriacao = DateTime.UtcNow;
    }

    public void EditarValor(decimal? novoValor, string atualizadoPor)
    {
        if (novoValor is < 0)
            throw new ArgumentException("Valor editado inválido.");

        ValorEditado = (novoValor.HasValue && novoValor != ValorPadrao)
            ? novoValor
            : null;

        AtualizadoPor = atualizadoPor ?? "Sistema";
        DataAtualizacao = DateTime.UtcNow;
    }

    public void AtualizarQuantidade(int total, int cortesia, string atualizadoPor)
    {
        if (total < 0 || cortesia < 0 || cortesia > total)
            throw new ArgumentException("Quantidade inválida.");

        QuantidadeTotal = total;
        QuantidadeCortesia = cortesia;

        AtualizadoPor = atualizadoPor ?? "Sistema";
        DataAtualizacao = DateTime.UtcNow;
    }
}
