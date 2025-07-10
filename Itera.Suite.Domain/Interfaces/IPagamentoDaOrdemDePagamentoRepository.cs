using Itera.Suite.Domain.Entities;

namespace Itera.Suite.Domain.Interfaces;

public interface IPagamentoDaOrdemDePagamentoRepository
{
    Task<PagamentoDaOrdemDePagamento?> GetByIdAsync(Guid id);
    Task UpdateAsync(PagamentoDaOrdemDePagamento pagamentoDaOrdemDePagamento);
    // Exemplos futuros:
    // Task<IEnumerable<PagamentoProgramado>> GetAtrasadosPorProjeto(Guid projetoId);
    // Task AddAsync(PagamentoProgramado pagamento);
}