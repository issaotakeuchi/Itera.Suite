namespace Itera.Suite.Application.Interfaces;

public interface IArquivoStorageService
{
    /// <summary>
    /// Faz upload de um arquivo e retorna o URL ou ID salvo.
    /// </summary>
    /// <param name="fileName">Nome do arquivo</param>
    /// <param name="content">Stream com o conteúdo</param>
    /// <param name="contentType">MIME Type</param>
    /// <returns>URL ou ID</returns>
    Task<string> UploadAsync(string fileName, Stream content, string contentType);

    /// <summary>
    /// Baixa um arquivo a partir do ID ou caminho salvo.
    /// </summary>
    /// <param name="fileId">ID/path do arquivo</param>
    /// <returns>Stream do arquivo</returns>
    Task<Stream> DownloadAsync(string fileId);

    /// <summary>
    /// Exclui um arquivo do storage.
    /// </summary>
    /// <param name="fileId">ID/path do arquivo</param>
    /// <returns></returns>
    Task DeleteAsync(string fileId);
}
