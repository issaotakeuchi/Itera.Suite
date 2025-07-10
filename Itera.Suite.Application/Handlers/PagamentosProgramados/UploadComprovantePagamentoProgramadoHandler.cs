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

        var fileName = $"{Guid.NewGuid()}_{request.Arquivo.FileName}";
        var contentType = request.Arquivo.ContentType;

        // ⚡ Blindagem: bufferiza no MemoryStream
        await using var input = request.Arquivo.OpenReadStream();
        await using var memory = new MemoryStream();
        await input.CopyToAsync(memory);

        // ⚡ IMPORTANTE! Volta pro início antes de ToArray()
        memory.Position = 0;

        var bytes = memory.ToArray();

        // ✅ LOG: veja tamanho real e posição
        Console.WriteLine($"[DEBUG] MemoryStream Length: {memory.Length}");
        Console.WriteLine($"[DEBUG] MemoryStream Position: {memory.Position}");

        var url = await _arquivoStorageService.UploadAsync(fileName, memory, contentType);

        pagamento.ComprovanteUrl = url;
        await _pagamentoRepo.UpdateAsync(pagamento);

        Console.WriteLine($"[DEBUG] Upload finalizado: {url}");

        return url;
    }
}
