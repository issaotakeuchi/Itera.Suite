using Itera.Suite.Domain.Common;
using Itera.Suite.Shared.Enums;

namespace Itera.Suite.Domain.Entities;

public class BaseDeCalculo : Entity
{
    public string Nome { get; private set; }
    public bool Confirmada { get; private set; }
    public decimal Markup { get; private set; }
    public string CriadaPor { get; private set; }
    public DateTime DataCriacao { get; private set; }

    // Quantidades específicas desta base
    public int QtdAlunos { get; private set; }
    public int QtdProfessores { get; private set; }
    public int QtdGuias { get; private set; }
    public int QtdMotoristas { get; private set; }
    public bool MotoristaPermanece { get; private set; }

    // Itens aplicados
    private readonly List<ItemDeCustoAplicado> _itens = new();
    public IReadOnlyCollection<ItemDeCustoAplicado> Itens => _itens.AsReadOnly();

    public Guid ProjetoDeViagemId { get; private set; }  
    public ProjetoDeViagem ProjetoDeViagem { get; private set; } = null!;

    public decimal Total => _itens.Sum(i => i.Total);
    public decimal TotalComMarkup => Total * (1 + Markup / 100);
    public decimal LucroEstimado => TotalComMarkup - Total;

    protected BaseDeCalculo() { }

    public BaseDeCalculo(string nome, decimal markup, string criadaPor)
    {
        if (markup < 0 || markup > 1000)
            throw new ArgumentOutOfRangeException(nameof(markup), "Markup fora do intervalo permitido.");

        Nome = nome ?? throw new ArgumentNullException(nameof(nome));
        Markup = markup;
        CriadaPor = criadaPor ?? "Sistema";
        DataCriacao = DateTime.UtcNow;
    }

    public void DefinirQuantidades(int alunos, int professores, int guias, int motoristas, bool motoristaPermanece)
    {
        QtdAlunos = alunos;
        QtdProfessores = professores;
        QtdGuias = guias;
        QtdMotoristas = motoristas;
        MotoristaPermanece = motoristaPermanece;
    }

    public void AdicionarItem(
        ItemDeCusto item,
        string subtipo,
        int? quantidadeManual,
        int quantidadeCortesia,
        decimal? valorEditado,
        bool consideraNoLucro,
        string criadoPor)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));

        // Sugestão baseada no tipo de cálculo
        int quantidadeSugerida = item.TipoCalculo switch
        {
            TipoCalculoItem.PorPessoa => QtdAlunos,
            TipoCalculoItem.PorItem => 1,
            _ => 1
        };

        var itemAplicado = new ItemDeCustoAplicado(
            baseDeCalculo: this,
            item: item,
            subtipo: subtipo,
            quantidadeTotal: quantidadeManual ?? quantidadeSugerida,
            quantidadeCortesia: quantidadeCortesia,
            valorEditado: valorEditado,
            consideraNoLucro: consideraNoLucro,
            criadoPor: criadoPor
        );

        _itens.Add(itemAplicado);
    }

    public void Confirmar()
    {
        Confirmada = true;
    }

    public void Desconfirmar()
    {
        Confirmada = false;
    }
}
