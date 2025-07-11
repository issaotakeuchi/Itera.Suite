using Itera.Suite.Application.Commands.Clientes;
using Itera.Suite.Application.Handlers.Clientes;
using Microsoft.AspNetCore.Mvc;

namespace Itera.Suite.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    private readonly CriarClienteCommandHandler _handler;

    public ClientesController(CriarClienteCommandHandler handler)
    {
        _handler = handler;
    }

    [HttpPost]
    public async Task<IActionResult> Post(CriarClienteCommand command)
    {
        var id = await _handler.HandleAsync(command);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    // Exemplo GET simples
    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        // Pra completar depois, usa IClienteRepository pra retornar detalhes
        return Ok(new { id });
    }
}
