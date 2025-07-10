using Itera.Suite.Domain.Entities;
using Itera.Suite.Domain.Interfaces;
using Itera.Suite.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Itera.Suite.Infrastructure.Repositories;

public class PagamentoDaOrdemDePagamentoRepository : IPagamentoDaOrdemDePagamentoRepository
{
    private readonly AppDbContext _context;

    public PagamentoDaOrdemDePagamentoRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<PagamentoDaOrdemDePagamento?> GetByIdAsync(Guid id)
    {
        return _context.PagamentosDaOrdemDePagamento
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task UpdateAsync(PagamentoDaOrdemDePagamento pagamentoDaOrdemDePagamento)
    {
        _context.PagamentosDaOrdemDePagamento.Update(pagamentoDaOrdemDePagamento);
        await _context.SaveChangesAsync();
    }
}
