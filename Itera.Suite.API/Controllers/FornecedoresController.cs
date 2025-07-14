using Itera.Suite.Application.Commands.Fornecedores;
using Itera.Suite.Application.Handlers.Fornecedores;
using Itera.Suite.Application.Interfaces;
using Itera.Suite.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Itera.Suite.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FornecedoresController : ControllerBase
{
    private readonly CriarFornecedorCommandHandler _criarHandler;
    private readonly AtualizarFornecedorCommandHandler _atualizarHandler;
    private readonly IFornecedorRepository _repository; // Entity Commands
    private readonly IFornecedorQuery _query;           // DTO Queries

    public FornecedoresController(
        CriarFornecedorCommandHandler criarHandler,
        AtualizarFornecedorCommandHandler atualizarHandler,
        IFornecedorRepository repository,
        IFornecedorQuery query)
    {
        _criarHandler = criarHandler;
        _atualizarHandler = atualizarHandler;
        _repository = repository;
        _query = query;
    }

    [HttpPost]
    public async Task<IActionResult> Post(CriarFornecedorCommand command)
    {
        var id = await _criarHandler.HandleAsync(command);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, AtualizarFornecedorCommand command)
    {
        if (id != command.Id)
            return BadRequest("Id da rota não confere com o payload.");

        await _atualizarHandler.HandleAsync(command);
        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var fornecedores = await _query.ListarTodosAsync();
        return Ok(fornecedores);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var fornecedor = await _query.ObterPorIdProjectionAsync(id);
        if (fornecedor == null) return NotFound();
        return Ok(fornecedor);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var fornecedor = await _repository.ObterPorIdAsync(id);
        if (fornecedor == null) return NotFound();

        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var atualizadoPor = string.IsNullOrWhiteSpace(userId) ? "Sistema" : userId;

        fornecedor.Inativar(atualizadoPor);
        await _repository.SalvarAlteracoesAsync();

        return NoContent();
    }
}
