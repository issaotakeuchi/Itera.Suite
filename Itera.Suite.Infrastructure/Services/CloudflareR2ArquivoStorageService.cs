using Amazon.S3;
using Amazon.S3.Model;
using Itera.Suite.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Itera.Suite.Infrastructure.Services;

public class CloudflareR2ArquivoStorageService : IArquivoStorageService
{
    private readonly IAmazonS3 _s3Client;
    private readonly string _bucketName;

    public CloudflareR2ArquivoStorageService(IConfiguration configuration)
    {
        var endpoint = configuration["Storage:Endpoint"];
        var accessKey = configuration["Storage:AccessKey"];
        var secretKey = configuration["Storage:SecretKey"];
        _bucketName = configuration["Storage:BucketName"];
        var region = configuration["Storage:Region"] ?? "auto";

        var config = new AmazonS3Config
        {
            ServiceURL = endpoint,
            ForcePathStyle = true, // ⚡ IMPORTANTE: R2 precisa disso!
            AuthenticationRegion = region
        };

        _s3Client = new AmazonS3Client(accessKey, secretKey, config);
    }

    public async Task<string> UploadAsync(string fileName, Stream content, string contentType)
    {
        var putRequest = new PutObjectRequest
        {
            BucketName = _bucketName,
            Key = fileName,
            InputStream = content,
            ContentType = contentType,
            CannedACL = S3CannedACL.PublicRead // ou Private se quiser URL signed
        };

        await _s3Client.PutObjectAsync(putRequest);

        return $"{_s3Client.Config.ServiceURL}/{_bucketName}/{fileName}";
    }

    public async Task<Stream> DownloadAsync(string fileId)
    {
        var response = await _s3Client.GetObjectAsync(_bucketName, fileId);
        var memory = new MemoryStream();
        await response.ResponseStream.CopyToAsync(memory);
        memory.Position = 0;
        return memory;
    }

    public async Task DeleteAsync(string fileId)
    {
        await _s3Client.DeleteObjectAsync(_bucketName, fileId);
    }
}
