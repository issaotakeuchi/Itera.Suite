namespace Itera.Suite.Domain.Interfaces;

public interface IClienteRepository
{
    Task AdicionarAsync(Cliente cliente);
    Task<Cliente?> ObterPorIdAsync(Guid id);
    Task<IEnumerable<Cliente>> ListarTodosAsync();
    Task SalvarAlteracoesAsync();
}
