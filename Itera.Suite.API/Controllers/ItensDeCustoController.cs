using Itera.Suite.Application.Commands.ItensDeCusto;
using Itera.Suite.Application.Handlers.ItensDeCusto;
using Microsoft.AspNetCore.Mvc;

namespace Itera.Suite.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItensDeCustoController : ControllerBase
{
    private readonly CriarItemDeCustoCommandHandler _handler;

    public ItensDeCustoController(CriarItemDeCustoCommandHandler handler)
    {
        _handler = handler;
    }

    [HttpPost]
    public async Task<IActionResult> Post(CriarItemDeCustoCommand command)
    {
        var id = await _handler.HandleAsync(command);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        return Ok(new { id });
    }
}

