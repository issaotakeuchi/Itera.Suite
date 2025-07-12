using Itera.Suite.Application.Commands.ItensDeCusto;
using Itera.Suite.Domain.Entities;
using Itera.Suite.Domain.Interfaces;
using Itera.Suite.Shared.Enums;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Itera.Suite.Application.Handlers.ItensDeCusto;

public class CriarItemDeCustoCommandHandler
{
    private readonly IItemDeCustoRepository _repository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CriarItemDeCustoCommandHandler(
        IItemDeCustoRepository repository,
        IHttpContextAccessor httpContextAccessor)
    {
        _repository = repository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Guid> HandleAsync(CriarItemDeCustoCommand command)
    {
        if (string.IsNullOrWhiteSpace(command.Categoria))
        {
            throw new ArgumentException("Categoria é obrigatória e não pode ser vazia.");
        }

        var categoria = Enum.Parse<CategoriaItemDeCusto>(command.Categoria, true);

        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var criadoPor = string.IsNullOrWhiteSpace(userId) ? "Sistema" : userId;

        var item = new ItemDeCusto(
            projetoDeViagemId: command.ProjetoDeViagemId,
            categoria: categoria,
            descricao: command.Descricao,
            diarias: command.Diarias,
            quantidade: command.Quantidade,
            valorUnitario: command.ValorUnitario,
            fornecedorId: command.FornecedorId,
            criadoPor: criadoPor
        );

        await _repository.AdicionarAsync(item);
        await _repository.SalvarAlteracoesAsync();

        return item.Id;
    }
}
