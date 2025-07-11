using Itera.Suite.Domain.Entities;

namespace Itera.Suite.Domain.Interfaces;

public interface IItemDeCustoRepository
{
    Task AdicionarAsync(ItemDeCusto item);
    Task<ItemDeCusto?> ObterPorIdAsync(Guid id);
    Task<IEnumerable<ItemDeCusto>> ListarTodosAsync();
    Task SalvarAlteracoesAsync();
}
