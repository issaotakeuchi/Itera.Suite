using Itera.Suite.Domain.Entities;

namespace Itera.Suite.Domain.Interfaces;

public interface IFornecedorRepository
{
    Task AdicionarAsync(Fornecedor fornecedor);
    Task<Fornecedor?> ObterPorIdAsync(Guid id);
    Task<IEnumerable<Fornecedor>> ListarTodosAsync();
    Task SalvarAlteracoesAsync();
}
