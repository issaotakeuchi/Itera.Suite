using Itera.Suite.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace Itera.Suite.Application.Handlers.BasesDeCalculo;

public class ConfirmarBaseHandler
{
    private readonly IBaseDeCalculoRepository _repository;

    public ConfirmarBaseHandler(IBaseDeCalculoRepository repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(Guid projetoId, Guid baseId)
    {
        var todasBases = await _repository.ObterPorProjetoIdAsync(projetoId);

        var baseSelecionada = todasBases.FirstOrDefault(b => b.Id == baseId);

        if (baseSelecionada is null)
            throw new InvalidOperationException("Base de cálculo não encontrada neste projeto");

        // Desconfirma todas
        foreach (var b in todasBases)
            b.Desconfirmar();

        baseSelecionada.Confirmar();

        await _repository.SalvarAlteracoesAsync();
    }
}
