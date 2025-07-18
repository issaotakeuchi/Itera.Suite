using Itera.Suite.Application.Commands.BasesDeCalculo;
using Itera.Suite.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Itera.Suite.Application.Handlers.BasesDeCalculo;

public class CriarBaseDeCalculoCommandHandler
{
    private readonly IProjetoDeViagemRepository _repository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CriarBaseDeCalculoCommandHandler(
        IProjetoDeViagemRepository repository,
        IHttpContextAccessor httpContextAccessor)
    {
        _repository = repository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task HandleAsync(CriarBaseDeCalculoCommand command)
    {
        var projeto = await _repository.ObterPorIdAsync(command.ProjetoDeViagemId)
            ?? throw new InvalidOperationException("Projeto não encontrado.");

        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var criadoPor = string.IsNullOrWhiteSpace(userId) ? "Sistema" : userId;

        projeto.CriarNovaBase(command.Nome, command.Markup, criadoPor);

        await _repository.SalvarAlteracoesAsync();
    }
}
