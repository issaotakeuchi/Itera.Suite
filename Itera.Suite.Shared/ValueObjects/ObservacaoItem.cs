using Itera.Suite.Domain.Entities;
using Itera.Suite.Shared.ValueObjects;

namespace Itera.Suite.Shared.ValueObjects;

public class ObservacaoItem : ObservacaoBase
{
    public Guid ItemDeCustoId { get; set; }
    public ItemDeCusto ItemDeCusto { get; set; }
}

