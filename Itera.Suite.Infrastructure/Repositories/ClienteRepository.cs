using Itera.Suite.Application.DTOs;
using Itera.Suite.Application.Interfaces;
using Itera.Suite.Domain.Interfaces;
using Itera.Suite.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Itera.Suite.Infrastructure.Repositories;

public class ClienteRepository : IClienteRepository, IClienteQuery
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

    public async Task<Cliente?> ObterPorIdAsync(Guid id) =>
        await _context.Clientes
            .Include(c => c.Projetos)
            .FirstOrDefaultAsync(c => c.Id == id);

    public async Task<IEnumerable<ClienteDto>> ListarTodosAsync()
        => await _context.Clientes
            .AsNoTracking()
            .Select(c => new ClienteDto
            {
                Id = c.Id,
                Nome = c.Nome,
                Email = c.Email,
                ContatoPrincipal = c.ContatoPrincipal,
                Documento = c.Documento, // CPF/CNPJ
                Telefone = c.Telefone,
                Projetos = c.Projetos.Select(p => new ProjetoResumoDto
                {
                    Id = p.Id,
                    NomeInterno = p.NomeInterno
                }).ToList()
            }).ToListAsync();

    public async Task<ClienteDto?> ObterPorIdProjectionAsync(Guid id)
        => await _context.Clientes
            .AsNoTracking()
            .Where(c => c.Id == id)
            .Select(c => new ClienteDto
            {
                Id = c.Id,
                Nome = c.Nome,
                Email = c.Email,
                ContatoPrincipal = c.ContatoPrincipal,
                Documento = c.Documento, // CPF/CNPJ
                Telefone = c.Telefone,
                Projetos = c.Projetos.Select(p => new ProjetoResumoDto
                {
                    Id = p.Id,
                    NomeInterno = p.NomeInterno
                }).ToList()
            }).FirstOrDefaultAsync();

    public async Task SalvarAlteracoesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
