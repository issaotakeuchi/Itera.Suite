using Itera.Suite.Application.DTOs;

namespace Itera.Suite.Application.Interfaces;

public interface IItemDeCustoQuery
{
    Task<IEnumerable<ItemDeCustoDto>> ListarTodosAsync();
    Task<ItemDeCustoDto?> ObterPorIdProjectionAsync(Guid id);
}
