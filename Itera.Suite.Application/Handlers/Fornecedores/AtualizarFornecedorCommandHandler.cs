using Itera.Suite.Application.Commands.Fornecedores;
using Itera.Suite.Domain.Interfaces;
using Itera.Suite.Shared.Enums;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Itera.Suite.Application.Handlers.Fornecedores;

public class AtualizarFornecedorCommandHandler
{
    private readonly IFornecedorRepository _repository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AtualizarFornecedorCommandHandler(
        IFornecedorRepository repository,
        IHttpContextAccessor httpContextAccessor)
    {
        _repository = repository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task HandleAsync(AtualizarFornecedorCommand command)
    {
        var fornecedor = await _repository.ObterPorIdAsync(command.Id)
            ?? throw new InvalidOperationException("Fornecedor não encontrado.");

        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var atualizadoPor = string.IsNullOrWhiteSpace(userId) ? "Sistema" : userId;

        TipoServico? tipo = null;
        if (!string.IsNullOrWhiteSpace(command.TipoDeServico))
            tipo = Enum.Parse<TipoServico>(command.TipoDeServico, true);

        fornecedor.AtualizarDados(
            nome: command.Nome,
            contato: command.Contato,
            email: command.Email,
            telefone: command.Telefone,
            tipo: tipo,
            atualizadoPor: atualizadoPor
        );

        await _repository.SalvarAlteracoesAsync();
    }
}

