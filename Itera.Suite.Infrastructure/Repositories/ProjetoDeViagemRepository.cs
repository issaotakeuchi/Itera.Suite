﻿using Itera.Suite.Application.DTOs;
using Itera.Suite.Application.Interfaces;
using Itera.Suite.Domain.Entities;
using Itera.Suite.Domain.Interfaces;
using Itera.Suite.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Itera.Suite.Infrastructure.Repositories;

public class ProjetoDeViagemRepository : IProjetoDeViagemRepository, IProjetoDeViagemQuery
{
    private readonly AppDbContext _context;

    public ProjetoDeViagemRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ProjetoDeViagem?> ObterPorIdAsync(Guid id)
    {
        return await _context.ProjetosDeViagem
            .Include(p => p.Cliente)
            .Include(p => p.Bases)
                .ThenInclude(b => b.Itens)
                    .ThenInclude(i => i.ItemDeCusto)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task AdicionarAsync(ProjetoDeViagem projeto)
    {
        await _context.ProjetosDeViagem.AddAsync(projeto);
    }

    public async Task SalvarAlteracoesAsync()
    {
        await _context.SaveChangesAsync();
    }

    // Query (DTO)
    public async Task<IEnumerable<ProjetoDeViagemDto>> ListarTodosAsync()
    {
        var projetos = await _context.ProjetosDeViagem
            .AsNoTracking()
            .Include(p => p.Bases)
            .Include(p => p.Cliente)
            .ToListAsync();

        return projetos.Select(p => new ProjetoDeViagemDto
        {
            Id = p.Id,
            NomeInterno = p.NomeInterno,
            Origem = p.Origem,
            Destino = p.Destino,
            Objetivo = p.Objetivo,
            Tipo = p.Tipo.ToString(),
            Status = p.Status.ToString(),
            DataSaida = p.DataSaida,
            DataRetorno = p.DataRetorno,
            ClienteNome = p.Cliente.Nome,
            ClienteId = p.ClienteId,
            ValorTotalProvisionado = p.Bases.FirstOrDefault(b => b.Confirmada)?.TotalComMarkup ?? 0
        });
    }

    public async Task<ProjetoDeViagemDto?> ObterPorIdProjectionAsync(Guid id)
        => await _context.ProjetosDeViagem
            .AsNoTracking()
            .Where(p => p.Id == id)
            .Select(p => new ProjetoDeViagemDto
            {
                Id = p.Id,
                NomeInterno = p.NomeInterno,
                Origem = p.Origem,
                Destino = p.Destino,
                Objetivo = p.Objetivo,
                Tipo = p.Tipo.ToString(),
                Status = p.Status.ToString(),
                DataSaida = p.DataSaida,
                DataRetorno = p.DataRetorno,
                ClienteNome = p.Cliente.Nome
            })
            .FirstOrDefaultAsync();

    public async Task<ProjetoDeViagem?> ObterPorIdComItensAsync(Guid id)
    {
        return await _context.ProjetosDeViagem
            .Include(p => p.Cliente)
            .Include(p => p.Bases)
                .ThenInclude(b => b.Itens)
                    .ThenInclude(i => i.ItemDeCusto)
                        .ThenInclude(i => i.Fornecedor)
            .Include(p => p.Bases)
                .ThenInclude(b => b.Itens)
                    .ThenInclude(i => i.ItemDeCusto)
                        .ThenInclude(i => i.Pagamentos)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
}
