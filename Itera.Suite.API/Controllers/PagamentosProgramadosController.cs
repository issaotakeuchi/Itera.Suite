using Itera.Suite.Application.Commands.PagamentosProgramados;
using Itera.Suite.Domain.Entities;
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

    [HttpPost("{pagamentoDaOrdemDePagamentoId}/comprovante")]
    public async Task<IActionResult> UploadComprovante(Guid pagamentoDaOrdemDePagamentoId, IFormFile arquivo)
    {
        if (arquivo == null || arquivo.Length == 0)
            return BadRequest("Arquivo inválido.");

        var command = new UploadComprovantePagamentoDaOrdemDePagamentoCommand
        {
            PagamentoDaOrdemDePagamentoId = pagamentoDaOrdemDePagamentoId,
            Arquivo = arquivo
        };

        var comprovanteUrl = await _mediator.Send(command);

        return Ok(new { comprovanteUrl });
    }
}
