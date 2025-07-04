
using Itera.Suite.Application.Commands.ProjetosDeViagem;
using Itera.Suite.Application.Handlers.ProjetosDeViagem;
using Microsoft.AspNetCore.Mvc;

namespace Itera.Suite.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjetoDeViagemController : ControllerBase
{
    private readonly CriarProjetoDeViagemCommandHandler _handler;

    public ProjetoDeViagemController(CriarProjetoDeViagemCommandHandler handler)
    {
        _handler = handler;
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarProjetoDeViagemCommand command)
    {
        var id = await _handler.HandleAsync(command);
        return CreatedAtAction(nameof(Criar), new { id }, new { id });
    }
}
