using Itera.Suite.Application.Commands.PagamentosProgramados;
using Itera.Suite.Application.Common.Exceptions;
using Itera.Suite.Application.Interfaces;
using Itera.Suite.Domain.Interfaces;
using MediatR;

namespace Itera.Suite.Application.Handlers.PagamentosProgramados;

public class UploadComprovantePagamentoProgramadoHandler : IRequestHandler<UploadComprovantePagamentoProgramadoCommand, string>
{
    private readonly IArquivoStorageService _arquivoStorageService;
    private readonly IPagamentoProgramadoRepository _pagamentoRepo;

    public UploadComprovantePagamentoProgramadoHandler(
        IArquivoStorageService arquivoStorageService,
        IPagamentoProgramadoRepository pagamentoRepo)
    {
        _arquivoStorageService = arquivoStorageService;
        _pagamentoRepo = pagamentoRepo;
    }

    public async Task<string> Handle(UploadComprovantePagamentoProgramadoCommand request, CancellationToken cancellationToken)
    {
        var pagamento = await _pagamentoRepo.GetByIdAsync(request.PagamentoProgramadoId)
            ?? throw new NotFoundException("Pagamento Programado não encontrado.");

        using var stream = request.Arquivo.OpenReadStream();
        var fileName = $"{Guid.NewGuid()}_{request.Arquivo.FileName}";
        var contentType = request.Arquivo.ContentType;

        var url = await _arquivoStorageService.UploadAsync(fileName, stream, contentType);

        pagamento.ComprovanteUrl = url;

        await _pagamentoRepo.UpdateAsync(pagamento); // Ou UnitOfWork se usar.

        return url;
    }
}