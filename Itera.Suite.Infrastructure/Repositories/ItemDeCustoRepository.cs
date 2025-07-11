using Itera.Suite.Domain.Entities;
using Itera.Suite.Domain.Interfaces;
using Itera.Suite.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace Itera.Suite.Infrastructure.Repositories;


public class ItemDeCustoRepository : IItemDeCustoRepository
{
    private readonly AppDbContext _context;

    public ItemDeCustoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AdicionarAsync(ItemDeCusto item)
    {
        await _context.ItensDeCusto.AddAsync(item);
    }

    public async Task<ItemDeCusto?> ObterPorIdAsync(Guid id)
    {
        return await _context.ItensDeCusto.FindAsync(id);
    }

    public async Task<IEnumerable<ItemDeCusto>> ListarTodosAsync()
    {
        return await _context.ItensDeCusto.AsNoTracking().ToListAsync();
    }

    public async Task SalvarAlteracoesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
