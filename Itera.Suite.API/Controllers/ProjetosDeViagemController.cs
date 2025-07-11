using Itera.Suite.Application.Commands.ProjetosDeViagem;
using Itera.Suite.Application.Handlers.ProjetosDeViagem;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProjetosDeViagemController : ControllerBase
{
    private readonly CriarProjetoDeViagemCommandHandler _handler;

    public ProjetosDeViagemController(CriarProjetoDeViagemCommandHandler handler)
    {
        _handler = handler;
    }

    [HttpPost]
    public async Task<IActionResult> Post(CriarProjetoDeViagemCommand command)
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
