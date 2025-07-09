using Itera.Suite.Domain.Entities;

namespace Itera.Suite.Domain.Interfaces;

public interface IPagamentoProgramadoRepository
{
    Task<PagamentoProgramado?> GetByIdAsync(Guid id);
    Task UpdateAsync(PagamentoProgramado pagamento);
    // Exemplos futuros:
    // Task<IEnumerable<PagamentoProgramado>> GetAtrasadosPorProjeto(Guid projetoId);
    // Task AddAsync(PagamentoProgramado pagamento);
}