using Itera.Suite.Application.Commands.ProjetosDeViagem;
using Itera.Suite.Application.Handlers.ProjetosDeViagem;
using Itera.Suite.Application.Interfaces;
using Itera.Suite.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
public class ProjetosDeViagemController : ControllerBase
{
    private readonly CriarProjetoDeViagemCommandHandler _criarHandler;
    private readonly AtualizarProjetoDeViagemCommandHandler _atualizarHandler;
    private readonly IProjetoDeViagemRepository _repository; // Entity Commands
    private readonly IProjetoDeViagemQuery _query;           // DTO Query

    public ProjetosDeViagemController(
        CriarProjetoDeViagemCommandHandler criarHandler,
        AtualizarProjetoDeViagemCommandHandler atualizarHandler,
        IProjetoDeViagemRepository repository,
        IProjetoDeViagemQuery query)
    {
        _criarHandler = criarHandler;
        _atualizarHandler = atualizarHandler;
        _repository = repository;
        _query = query;
    }

    [HttpPost]
    public async Task<IActionResult> Post(CriarProjetoDeViagemCommand command)
    {
        var id = await _criarHandler.HandleAsync(command);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, AtualizarProjetoDeViagemCommand command)
    {
        if (id != command.Id)
            return BadRequest("Id da rota não confere com o payload.");

        await _atualizarHandler.HandleAsync(command);
        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var projetos = await _query.ListarTodosAsync();
        return Ok(projetos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var projeto = await _query.ObterPorIdProjectionAsync(id);
        if (projeto == null) return NotFound();
        return Ok(projeto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var projeto = await _repository.ObterPorIdAsync(id);
        if (projeto == null) return NotFound();

        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var atualizadoPor = string.IsNullOrWhiteSpace(userId) ? "Sistema" : userId;

        projeto.Inativar(atualizadoPor);

        await _repository.SalvarAlteracoesAsync();

        return NoContent();
    }
}
