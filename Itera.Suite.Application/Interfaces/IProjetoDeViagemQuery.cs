namespace Itera.Suite.Application.Interfaces;

using Itera.Suite.Application.DTOs;

public interface IProjetoDeViagemQuery
{
    Task<IEnumerable<ProjetoDeViagemDto>> ListarTodosAsync();
    Task<ProjetoDeViagemDto?> ObterPorIdProjectionAsync(Guid id);
}
