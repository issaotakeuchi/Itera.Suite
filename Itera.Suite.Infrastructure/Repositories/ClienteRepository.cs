using Itera.Suite.Domain.Interfaces;
using Itera.Suite.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Itera.Suite.Infrastructure.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly AppDbContext _context;

    public ClienteRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AdicionarAsync(Cliente cliente)
    {
        await _context.Clientes.AddAsync(cliente);
    }

    public async Task<Cliente?> ObterPorIdAsync(Guid id)
    {
        return await _context.Clientes
            .Include(c => c.Projetos) // se quiser trazer os projetos já vinculados
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Cliente>> ListarTodosAsync()
    {
        return await _context.Clientes
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task SalvarAlteracoesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
