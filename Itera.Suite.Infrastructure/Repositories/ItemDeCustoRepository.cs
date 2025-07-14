using Itera.Suite.Application.DTOs;
using Itera.Suite.Application.Interfaces;
using Itera.Suite.Domain.Entities;
using Itera.Suite.Domain.Interfaces;
using Itera.Suite.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace Itera.Suite.Infrastructure.Repositories;


public class ItemDeCustoRepository : IItemDeCustoRepository, IItemDeCustoQuery
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
        => await _context.ItensDeCusto
            .Include(i => i.Fornecedor)
            .Include(i => i.ProjetoDeViagem)
            .FirstOrDefaultAsync(i => i.Id == id);

    public async Task SalvarAlteracoesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<ItemDeCustoDto>> ListarTodosAsync()
        => await _context.ItensDeCusto
            .AsNoTracking()
            .Select(i => new ItemDeCustoDto
            {
                Id = i.Id,
                Categoria = i.Categoria.ToString(),
                Descricao = i.Descricao,
                Diarias = i.Diarias,
                Quantidade = i.Quantidade,
                ValorUnitario = i.ValorUnitario,
                Total = i.Total,
                StatusAtual = i.StatusAtual.ToString(),
                FornecedorNome = i.Fornecedor != null ? i.Fornecedor.Nome : null,
                ProjetoNome = i.ProjetoDeViagem.NomeInterno
            }).ToListAsync();

    public async Task<ItemDeCustoDto?> ObterPorIdProjectionAsync(Guid id)
        => await _context.ItensDeCusto
            .AsNoTracking()
            .Where(i => i.Id == id)
            .Select(i => new ItemDeCustoDto
            {
                Id = i.Id,
                Categoria = i.Categoria.ToString(),
                Descricao = i.Descricao,
                Diarias = i.Diarias,
                Quantidade = i.Quantidade,
                ValorUnitario = i.ValorUnitario,
                Total = i.Total,
                StatusAtual = i.StatusAtual.ToString(),
                FornecedorNome = i.Fornecedor != null ? i.Fornecedor.Nome : null,
                ProjetoNome = i.ProjetoDeViagem.NomeInterno
            }).FirstOrDefaultAsync();
}
