using Itera.Suite.Domain.Entities;
using Itera.Suite.Domain.Interfaces;
using Itera.Suite.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Itera.Suite.Infrastructure.Repositories;

public class PagamentoProgramadoRepository : IPagamentoProgramadoRepository
{
    private readonly AppDbContext _context;

    public PagamentoProgramadoRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<PagamentoProgramado?> GetByIdAsync(Guid id)
    {
        return _context.PagamentosProgramados
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task UpdateAsync(PagamentoProgramado pagamento)
    {
        _context.PagamentosProgramados.Update(pagamento);
        await _context.SaveChangesAsync();
    }
}
