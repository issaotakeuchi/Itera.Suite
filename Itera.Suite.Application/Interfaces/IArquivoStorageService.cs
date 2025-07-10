namespace Itera.Suite.Application.Interfaces
{
    public interface IArquivoStorageService
    {
        /// <summary>
        /// Faz upload de um arquivo como Stream. A implementação pode bufferizar internamente.
        /// </summary>
        Task<string> UploadAsync(string fileName, Stream content, string contentType);

        /// <summary>
        /// Baixa um arquivo salvo no storage.
        /// </summary>
        Task<Stream> DownloadAsync(string fileId);

        /// <summary>
        /// Remove um arquivo do storage.
        /// </summary>
        Task DeleteAsync(string fileId);
    }
}
