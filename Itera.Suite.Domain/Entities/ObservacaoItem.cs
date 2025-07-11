using Itera.Suite.Domain.Common;

namespace Itera.Suite.Domain.Entities;

public class ObservacaoItem : ObservacaoBase
{
    public Guid ItemDeCustoId { get; set; }
    public ItemDeCusto ItemDeCusto { get; set; }
}

