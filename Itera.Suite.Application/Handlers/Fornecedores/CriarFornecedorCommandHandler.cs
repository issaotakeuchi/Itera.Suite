using Itera.Suite.Application.Commands.Fornecedores;
using Itera.Suite.Domain.Interfaces;
using Itera.Suite.Shared.Enums;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Itera.Suite.Application.Handlers.Fornecedores;

public class CriarFornecedorCommandHandler
{
    private readonly IFornecedorRepository _repository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CriarFornecedorCommandHandler(
        IFornecedorRepository repository,
        IHttpContextAccessor httpContextAccessor)
    {
        _repository = repository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Guid> HandleAsync(CriarFornecedorCommand command)
    {
        TipoServico? tipoServico = null;
        if (!string.IsNullOrWhiteSpace(command.TipoDeServico))
        {
            tipoServico = Enum.Parse<TipoServico>(command.TipoDeServico, true);
        }

        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var criadoPor = string.IsNullOrWhiteSpace(userId) ? "Sistema" : userId;

        var fornecedor = new Fornecedor(
            nome: command.Nome,
            criadoPor: criadoPor,
            contato: command.Contato,
            email: command.Email,
            telefone: command.Telefone,
            tipoDeServico: tipoServico
        );

        await _repository.AdicionarAsync(fornecedor);
        await _repository.SalvarAlteracoesAsync();

        return fornecedor.Id;
    }
}
