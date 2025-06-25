using Itera.Suite.Domain.Enums;

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

    public ICollection<ItemDeCusto> ItensDeCusto { get; set; } = new List<ItemDeCusto>();
}

