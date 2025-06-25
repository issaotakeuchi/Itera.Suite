using Itera.Suite.Domain.Entities;

namespace Itera.Suite.Domain.ValueObjects;

public class ObservacaoItem : ObservacaoBase
{
    public Guid ItemDeCustoId { get; set; }
    public ItemDeCusto ItemDeCusto { get; set; }
}

