using Itera.Suite.Application.Commands.PagamentosProgramados;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Itera.Suite.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PagamentosProgramadosController : ControllerBase
{
    private readonly IMediator _mediator;

    public PagamentosProgramadosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("{pagamentoProgramadoId}/comprovante")]
    public async Task<IActionResult> UploadComprovante(Guid pagamentoProgramadoId, IFormFile arquivo)
    {
        if (arquivo == null || arquivo.Length == 0)
            return BadRequest("Arquivo inválido.");

        var command = new UploadComprovantePagamentoProgramadoCommand
        {
            PagamentoProgramadoId = pagamentoProgramadoId,
            Arquivo = arquivo
        };

        var comprovanteUrl = await _mediator.Send(command);

        return Ok(new { comprovanteUrl });
    }
}
