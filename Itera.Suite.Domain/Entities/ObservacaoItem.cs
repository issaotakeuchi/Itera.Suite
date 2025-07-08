using Itera.Suite.Domain.ValueObjects;

namespace Itera.Suite.Domain.Entities;

public class ObservacaoItem : ObservacaoBase
{
    public Guid ItemDeCustoId { get; set; }
    public ItemDeCusto ItemDeCusto { get; set; }
}

