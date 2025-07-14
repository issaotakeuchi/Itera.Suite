using Itera.Suite.Application.Commands.ItensDeCusto;
using Itera.Suite.Domain.Interfaces;
using Itera.Suite.Shared.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Itera.Suite.Application.Handlers.ItensDeCusto;

public class AtualizarItemDeCustoCommandHandler
{
    private readonly IItemDeCustoRepository _repository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AtualizarItemDeCustoCommandHandler(
        IItemDeCustoRepository repository,
        IHttpContextAccessor httpContextAccessor)
    {
        _repository = repository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task HandleAsync(AtualizarItemDeCustoCommand command)
    {
        var item = await _repository.ObterPorIdAsync(command.Id)
            ?? throw new InvalidOperationException("Item não encontrado.");

        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var atualizadoPor = string.IsNullOrWhiteSpace(userId) ? "Sistema" : userId;

        CategoriaItemDeCusto? categoria = null;
        if (!string.IsNullOrWhiteSpace(command.Categoria))
            categoria = Enum.Parse<CategoriaItemDeCusto>(command.Categoria, true);

        item.AtualizarDados(
            categoria: categoria,
            descricao: command.Descricao,
            diarias: command.Diarias,
            quantidade: command.Quantidade,
            valorUnitario: command.ValorUnitario,
            fornecedorId: command.FornecedorId,
            atualizadoPor: atualizadoPor
        );

        await _repository.SalvarAlteracoesAsync();
    }
}

