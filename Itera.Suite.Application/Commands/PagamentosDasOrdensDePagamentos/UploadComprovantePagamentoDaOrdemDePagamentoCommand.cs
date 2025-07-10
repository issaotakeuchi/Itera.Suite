using MediatR;
using Microsoft.AspNetCore.Http;

namespace Itera.Suite.Application.Commands.PagamentosProgramados;

public class UploadComprovantePagamentoDaOrdemDePagamentoCommand : IRequest<string>
{
    public Guid PagamentoDaOrdemDePagamentoId { get; set; }
    public IFormFile Arquivo { get; set; }
}
