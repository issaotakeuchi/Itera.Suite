using Itera.Suite.Application.Commands.Clientes;
using Itera.Suite.Application.Handlers.Clientes;
using Itera.Suite.Application.Interfaces;
using Itera.Suite.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    private readonly CriarClienteCommandHandler _criarHandler;
    private readonly AtualizarClienteCommandHandler _atualizarHandler;
    private readonly IClienteRepository _repository;
    private readonly IClienteQuery _query;

    public ClientesController(
        CriarClienteCommandHandler criarHandler,
        AtualizarClienteCommandHandler atualizarHandler,
        IClienteRepository repository,
         IClienteQuery query)
    {
        _criarHandler = criarHandler;
        _atualizarHandler = atualizarHandler;
        _repository = repository;
        _query = query;
    }

    [HttpPost]
    public async Task<IActionResult> Post(CriarClienteCommand command)
    {
        var id = await _criarHandler.HandleAsync(command);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, AtualizarClienteCommand command)
    {
        if (id != command.Id)
            return BadRequest("Id da rota não confere com o payload.");

        await _atualizarHandler.HandleAsync(command);
        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var clientes = await _query.ListarTodosAsync();
        return Ok(clientes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var cliente = await _query.ObterPorIdProjectionAsync(id);
        if (cliente == null) return NotFound();
        return Ok(cliente);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var cliente = await _repository.ObterPorIdAsync(id);
        if (cliente == null) return NotFound();

        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var atualizadoPor = string.IsNullOrWhiteSpace(userId) ? "Sistema" : userId;

        cliente.Inativar(atualizadoPor);

        await _repository.SalvarAlteracoesAsync();

        return NoContent();
    }
}
