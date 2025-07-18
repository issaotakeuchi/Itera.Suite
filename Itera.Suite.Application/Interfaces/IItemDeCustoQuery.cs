using Itera.Suite.Application.DTOs;

namespace Itera.Suite.Application.Interfaces;

public interface IItemDeCustoQuery
{
    Task<IEnumerable<ItemDeCustoDto>> ListarTodosAsync();
    Task<IEnumerable<ItemDeCustoDto>> ListarPorProjetoAsync(Guid projetoId);
    Task<ItemDeCustoDto?> ObterPorIdProjectionAsync(Guid id);
}
