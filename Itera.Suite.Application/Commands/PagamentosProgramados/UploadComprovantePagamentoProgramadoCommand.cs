using MediatR;
using Microsoft.AspNetCore.Http;

namespace Itera.Suite.Application.Commands.PagamentosProgramados;

public class UploadComprovantePagamentoProgramadoCommand : IRequest<string>
{
    public Guid PagamentoProgramadoId { get; set; }
    public IFormFile Arquivo { get; set; }
}
