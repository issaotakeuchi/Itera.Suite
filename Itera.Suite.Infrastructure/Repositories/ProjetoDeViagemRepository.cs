using Itera.Suite.Domain.Entities;
using Itera.Suite.Domain.Interfaces;
using Itera.Suite.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Itera.Suite.Infrastructure.Repositories;

public class ProjetoDeViagemRepository : IProjetoDeViagemRepository
{
    private readonly AppDbContext _context;

    public ProjetoDeViagemRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ProjetoDeViagem?> ObterPorIdAsync(Guid id)
    {
        return await _context.Projetos
            .Include(p => p.Cliente)
            .Include(p => p.ItensDeCusto)
                .ThenInclude(i => i.Pagamentos)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<ProjetoDeViagem>> ListarTodosAsync()
    {
        return await _context.Projetos
            .Include(p => p.Cliente)
            .ToListAsync();
    }

    public async Task AdicionarAsync(ProjetoDeViagem projeto)
    {
        await _context.Projetos.AddAsync(projeto);
    }

    public async Task SalvarAlteracoesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
