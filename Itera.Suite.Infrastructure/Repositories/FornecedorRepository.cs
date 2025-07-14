using Itera.Suite.Application.DTOs;
using Itera.Suite.Application.Interfaces;
using Itera.Suite.Domain.Interfaces;
using Itera.Suite.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Itera.Suite.Infrastructure.Repositories;

public class FornecedorRepository : IFornecedorRepository, IFornecedorQuery
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

    public async Task<IEnumerable<FornecedorDto>> ListarTodosAsync()
        => await _context.Fornecedores
            .AsNoTracking()
            .Select(f => new FornecedorDto
            {
                Id = f.Id,
                Nome = f.Nome,
                Contato = f.Contato,
                Email = f.Email,
                Telefone = f.Telefone,
                TipoDeServico = f.TipoDeServico.ToString()
            }).ToListAsync();

    public async Task<FornecedorDto?> ObterPorIdProjectionAsync(Guid id)
        => await _context.Fornecedores
            .AsNoTracking()
            .Where(f => f.Id == id)
            .Select(f => new FornecedorDto
            {
                Id = f.Id,
                Nome = f.Nome,
                Contato = f.Contato,
                Email = f.Email,
                Telefone = f.Telefone,
                TipoDeServico = f.TipoDeServico.ToString()
            }).FirstOrDefaultAsync();

    public async Task SalvarAlteracoesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
