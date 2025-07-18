using Itera.Suite.Domain.Entities;

namespace Itera.Suite.Domain.Interfaces;

public interface IBaseDeCalculoRepository
{
    Task<BaseDeCalculo?> ObterPorIdAsync(Guid id);
    Task<List<BaseDeCalculo>> ObterPorProjetoIdAsync(Guid projetoId);
    Task SalvarAlteracoesAsync();
}
