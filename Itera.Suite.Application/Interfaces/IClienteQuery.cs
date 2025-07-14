using Itera.Suite.Application.DTOs;

namespace Itera.Suite.Application.Interfaces;

public interface IClienteQuery
{
    Task<IEnumerable<ClienteDto>> ListarTodosAsync();
    Task<ClienteDto?> ObterPorIdProjectionAsync(Guid id);
}
