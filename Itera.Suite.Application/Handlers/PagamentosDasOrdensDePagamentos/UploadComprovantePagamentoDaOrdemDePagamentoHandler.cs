using Itera.Suite.Application.Commands.PagamentosProgramados;
using Itera.Suite.Application.Common.Exceptions;
using Itera.Suite.Application.Interfaces;
using Itera.Suite.Domain.Interfaces;
using MediatR;

namespace Itera.Suite.Application.Handlers.PagamentosProgramados;

public class UploadComprovantePagamentoDaOrdemDePagamentoHandler : IRequestHandler<UploadComprovantePagamentoDaOrdemDePagamentoCommand, string>
{
    private readonly IArquivoStorageService _arquivoStorageService;
    private readonly IPagamentoDaOrdemDePagamentoRepository _pagamentoDaOrdemDePagamentoRepo;

    public UploadComprovantePagamentoDaOrdemDePagamentoHandler(
        IArquivoStorageService arquivoStorageService,
        IPagamentoDaOrdemDePagamentoRepository pagamentoDaOrdemDePagamentoRepo)
    {
        _arquivoStorageService = arquivoStorageService;
        _pagamentoDaOrdemDePagamentoRepo = pagamentoDaOrdemDePagamentoRepo;
    }

    public async Task<string> Handle(UploadComprovantePagamentoDaOrdemDePagamentoCommand request, CancellationToken cancellationToken)
    {
        var pagamentoDaOrdemDePagamento = await _pagamentoDaOrdemDePagamentoRepo.GetByIdAsync(request.PagamentoDaOrdemDePagamentoId)
            ?? throw new NotFoundException("Pagamento da Ordem de Pagamento não encontrado.");

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

        pagamentoDaOrdemDePagamento.ComprovanteDaOrdemDePagamento.Url = url;
        await _pagamentoDaOrdemDePagamentoRepo.UpdateAsync(pagamentoDaOrdemDePagamento);

        Console.WriteLine($"[DEBUG] Upload finalizado: {url}");

        return url;
    }
}
