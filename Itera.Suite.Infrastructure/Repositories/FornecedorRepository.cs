using Itera.Suite.Domain.Entities;
using Itera.Suite.Domain.Interfaces;
using Itera.Suite.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Itera.Suite.Infrastructure.Repositories;

public class FornecedorRepository : IFornecedorRepository
{
    private readonly AppDbContext _context;

    public FornecedorRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AdicionarAsync(Fornecedor fornecedor)
    {
        await _context.Fornecedores.AddAsync(fornecedor);
    }

    public async Task<Fornecedor?> ObterPorIdAsync(Guid id)
    {
        return await _context.Fornecedores
            .Include(f => f.ItensDeCusto)
            .FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task<IEnumerable<Fornecedor>> ListarTodosAsync()
    {
        return await _context.Fornecedores
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task SalvarAlteracoesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
