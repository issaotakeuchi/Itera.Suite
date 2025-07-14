namespace Itera.Suite.Domain.Interfaces;

public interface IClienteRepository
{
    Task<Cliente?> ObterPorIdAsync(Guid id); // Entity real
    Task AdicionarAsync(Cliente cliente);
    Task SalvarAlteracoesAsync();
}
