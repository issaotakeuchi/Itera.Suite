using Itera.Suite.Application.Commands.ItensDeCusto;
using Itera.Suite.Application.DTOs;
using Itera.Suite.Application.Handlers.ItensDeCusto;
using Itera.Suite.Application.Interfaces;
using Itera.Suite.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Itera.Suite.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItensDeCustoController : ControllerBase
{
    private readonly CriarItemDeCustoCommandHandler _criarHandler;
    private readonly AtualizarItemDeCustoCommandHandler _atualizarHandler;
    private readonly AlterarStatusItemDeCustoHandler _alterarStatusHandler;
    private readonly IItemDeCustoRepository _repository; // Entity
    private readonly IItemDeCustoQuery _query;           // DTO

    public ItensDeCustoController(
        CriarItemDeCustoCommandHandler criarHandler,
        AtualizarItemDeCustoCommandHandler atualizarHandler,
        AlterarStatusItemDeCustoHandler alterarStatusItemDeCustoHandler,
        IItemDeCustoRepository repository,
        IItemDeCustoQuery query)
    {
        _criarHandler = criarHandler;
        _atualizarHandler = atualizarHandler;
        _alterarStatusHandler = alterarStatusItemDeCustoHandler;
        _repository = repository;
        _query = query;
    }

    [HttpPost]
    public async Task<IActionResult> Post(CriarItemDeCustoCommand command)
    {
        var id = await _criarHandler.HandleAsync(command);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var itens = await _query.ListarTodosAsync();
        return Ok(itens);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var item = await _query.ObterPorIdProjectionAsync(id);
        if (item == null) return NotFound();
        return Ok(item);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, AtualizarItemDeCustoCommand command)
    {
        if (id != command.Id)
            return BadRequest("Id da rota não confere com o payload.");

        await _atualizarHandler.HandleAsync(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var item = await _repository.ObterPorIdAsync(id);
        if (item == null) return NotFound();

        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var atualizadoPor = string.IsNullOrWhiteSpace(userId) ? "Sistema" : userId;

        item.Inativar(atualizadoPor);
        await _repository.SalvarAlteracoesAsync();

        return NoContent();
    }

    [HttpGet("projeto/{projetoId}")]
    public async Task<IActionResult> GetPorProjeto(Guid projetoId)
    {
        var itens = await _query.ListarPorProjetoAsync(projetoId);
        return Ok(itens);
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult> AlterarStatus(Guid id, [FromBody] AlterarStatusItemDeCustoCommand command)
    {
        if (id != command.Id)
            return BadRequest("Id da rota não confere com o payload.");

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "Sistema";

        await _alterarStatusHandler.HandleAsync(command, userId);
        return NoContent();
    }
}
