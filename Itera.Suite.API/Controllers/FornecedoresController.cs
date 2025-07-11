using Itera.Suite.Application.Commands.Fornecedores;
using Itera.Suite.Application.Handlers.Fornecedores;
using Microsoft.AspNetCore.Mvc;

namespace Itera.Suite.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FornecedoresController : ControllerBase
{
    private readonly CriarFornecedorCommandHandler _handler;

    public FornecedoresController(CriarFornecedorCommandHandler handler)
    {
        _handler = handler;
    }

    [HttpPost]
    public async Task<IActionResult> Post(CriarFornecedorCommand command)
    {
        var id = await _handler.HandleAsync(command);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        // Pra evoluir depois: usar o IFornecedorRepository
        return Ok(new { id });
    }
}
