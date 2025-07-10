using Itera.Suite.Domain.Entities;

namespace Itera.Suite.Domain.Interfaces;

public interface IOrdemDePagamentoRepository
{
    Task<OrdemDePagamento?> GetByIdAsync(Guid id);
    Task UpdateAsync(OrdemDePagamento pagamento);
    // Exemplos futuros:
    // Task<IEnumerable<PagamentoProgramado>> GetAtrasadosPorProjeto(Guid projetoId);
    // Task AddAsync(PagamentoProgramado pagamento);
}