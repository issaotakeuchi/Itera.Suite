using Itera.Suite.Domain.Entities;

namespace Itera.Suite.Domain.Interfaces;

public interface IProjetoDeViagemRepository
{
    Task<ProjetoDeViagem?> ObterPorIdAsync(Guid id);
    Task<List<ProjetoDeViagem>> ListarTodosAsync();
    Task AdicionarAsync(ProjetoDeViagem projeto);
    Task SalvarAlteracoesAsync();
}
