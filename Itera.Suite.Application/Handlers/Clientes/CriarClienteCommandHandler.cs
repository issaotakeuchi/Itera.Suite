using Itera.Suite.Application.Commands.Clientes;
using Itera.Suite.Domain.Interfaces;
using Itera.Suite.Shared.Enums;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Itera.Suite.Application.Handlers.Clientes;

public class CriarClienteCommandHandler
{
    private readonly IClienteRepository _repository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CriarClienteCommandHandler(
    IClienteRepository repository,
    IHttpContextAccessor httpContextAccessor)
    {
        _repository = repository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Guid> HandleAsync(CriarClienteCommand command)
    {
        // Parse defensivo do TipoCliente
        TipoCliente? tipoCliente = null;
        if (!string.IsNullOrWhiteSpace(command.Tipo))
        {
            tipoCliente = Enum.Parse<TipoCliente>(command.Tipo, true);
        }

        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var criadoPor = string.IsNullOrWhiteSpace(userId) ? "Sistema" : userId;

        var cliente = new Cliente(
            nome: command.Nome,
            documento: command.Documento,
            tipo: tipoCliente,
            contatoPrincipal: command.ContatoPrincipal,
            email: command.Email,
            telefone: command.Telefone,
            criadoPor: criadoPor
        );

        await _repository.AdicionarAsync(cliente);
        await _repository.SalvarAlteracoesAsync();

        return cliente.Id;
    }
}
