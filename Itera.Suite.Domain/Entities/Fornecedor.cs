using Itera.Suite.Shared.Enums;

namespace Itera.Suite.Domain.Entities;

public class Fornecedor
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Contato { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public TipoServico TipoDeServico { get; set; }
    public bool Ativo { get; set; } = true;
    public string CriadoPor { get; set; }
    public DateTime DataCriacao { get; set; }
    public string AtualizadoPor { get; set; }
    public DateTime? DataAtualizacao { get; set; }

    public ICollection<ItemDeCusto> ItensDeCusto { get; set; } = new List<ItemDeCusto>();
}

