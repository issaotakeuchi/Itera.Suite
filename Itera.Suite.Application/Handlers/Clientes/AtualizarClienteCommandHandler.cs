using Itera.Suite.Application.Commands.Clientes;
using Itera.Suite.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Itera.Suite.Application.Handlers.Clientes;

public class AtualizarClienteCommandHandler
{
    private readonly IClienteRepository _repository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AtualizarClienteCommandHandler(
        IClienteRepository repository,
        IHttpContextAccessor httpContextAccessor)
    {
        _repository = repository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task HandleAsync(AtualizarClienteCommand command)
    {
        var cliente = await _repository.ObterPorIdAsync(command.Id)
            ?? throw new InvalidOperationException("Cliente não encontrado.");

        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var atualizadoPor = string.IsNullOrWhiteSpace(userId) ? "Sistema" : userId;

        cliente.AtualizarDados(
            nome: command.Nome,
            documento: command.Documento,
            tipo: command.Tipo,
            contatoPrincipal: command.ContatoPrincipal,
            email: command.Email,
            telefone: command.Telefone,
            atualizadoPor: atualizadoPor
        );

        await _repository.SalvarAlteracoesAsync();
    }
}
