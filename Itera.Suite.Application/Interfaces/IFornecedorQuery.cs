using Itera.Suite.Application.DTOs;

namespace Itera.Suite.Application.Interfaces;

public interface IFornecedorQuery
{
    Task<IEnumerable<FornecedorDto>> ListarTodosAsync();
    Task<FornecedorDto?> ObterPorIdProjectionAsync(Guid id);
}
