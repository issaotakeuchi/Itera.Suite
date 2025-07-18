using Itera.Suite.Domain.Entities;
using Itera.Suite.Domain.Interfaces;
using Itera.Suite.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Itera.Suite.Infrastructure.Repositories;

public class BaseDeCalculoRepository : IBaseDeCalculoRepository
{
    private readonly AppDbContext _context;

    public BaseDeCalculoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<BaseDeCalculo?> ObterPorIdAsync(Guid id)
    {
        return await _context.BasesDeCalculo.FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<List<BaseDeCalculo>> ObterPorProjetoIdAsync(Guid projetoId)
    {
        return await _context.BasesDeCalculo
            .Where(b => b.ProjetoDeViagemId == projetoId)
            .ToListAsync();
    }

    public async Task SalvarAlteracoesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
