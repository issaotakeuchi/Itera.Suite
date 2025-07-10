using System.Net.Http.Headers;
using System.Text;
using Itera.Suite.Application.Interfaces;
using Itera.Suite.Infrastructure.Helpers;
using Microsoft.Extensions.Configuration;

namespace Itera.Suite.Infrastructure.Services;

public class CloudflareR2ArquivoStorageService : IArquivoStorageService
{
    private readonly string _endpoint;
    private readonly string _accessKey;
    private readonly string _secretKey;
    private readonly string _bucketName;
    private readonly string _region;

    public CloudflareR2ArquivoStorageService(IConfiguration configuration)
    {
        _endpoint = configuration["Storage:Endpoint"]!;
        _accessKey = configuration["Storage:AccessKey"]!;
        _secretKey = configuration["Storage:SecretKey"]!;
        _bucketName = configuration["Storage:BucketName"]!;
        _region = configuration["Storage:Region"] ?? "us-east-1";
    }

    public async Task<string> UploadAsync(string fileName, Stream content, string contentType)
    {
        await using var memory = new MemoryStream();
        await content.CopyToAsync(memory);
        var bytes = memory.ToArray();

        return await UploadWithHttpClientAsync(fileName, bytes, contentType);
    }

    private async Task<string> UploadWithHttpClientAsync(string fileName, byte[] bytes, string contentType)
    {
        Console.WriteLine($"[DEBUG] UploadBytesAsync Length: {bytes.Length}");

        var service = "s3";
        var host = new Uri(_endpoint).Host;
        var now = DateTime.UtcNow;
        var amzDate = now.ToString("yyyyMMddTHHmmssZ");
        var dateStamp = now.ToString("yyyyMMdd");
        var canonicalUri = $"/{_bucketName}/{Uri.EscapeDataString(fileName)}";
        var payloadHash = AwsV4Signer.Hash(bytes);

        var canonicalHeaders = $"host:{host}\n" +
                               $"x-amz-content-sha256:{payloadHash}\n" +
                               $"x-amz-date:{amzDate}\n";

        var signedHeaders = "host;x-amz-content-sha256;x-amz-date";
        var canonicalRequest = $"PUT\n{canonicalUri}\n\n{canonicalHeaders}\n{signedHeaders}\n{payloadHash}";

        var algorithm = "AWS4-HMAC-SHA256";
        var credentialScope = $"{dateStamp}/{_region}/{service}/aws4_request";
        var stringToSign = $"{algorithm}\n{amzDate}\n{credentialScope}\n{AwsV4Signer.Hash(Encoding.UTF8.GetBytes(canonicalRequest))}";

        var kDate = AwsV4Signer.HmacSHA256(dateStamp, Encoding.UTF8.GetBytes("AWS4" + _secretKey));
        var kRegion = AwsV4Signer.HmacSHA256(_region, kDate);
        var kService = AwsV4Signer.HmacSHA256(service, kRegion);
        var kSigning = AwsV4Signer.HmacSHA256("aws4_request", kService);
        var signatureBytes = AwsV4Signer.HmacSHA256(stringToSign, kSigning);
        var signature = BitConverter.ToString(signatureBytes).Replace("-", "").ToLower();

        var authorizationHeader = $"Credential={_accessKey}/{credentialScope}, SignedHeaders={signedHeaders}, Signature={signature}";

        using var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Put, $"{_endpoint}{canonicalUri}")
        {
            Content = new ByteArrayContent(bytes)
        };

        request.Content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
        request.Headers.Add("x-amz-content-sha256", payloadHash);
        request.Headers.Add("x-amz-date", amzDate);

        // ✅ CORRETO: AuthenticationHeaderValue separa o Scheme do restante!
        request.Headers.Authorization = new AuthenticationHeaderValue(
            algorithm,
            authorizationHeader
        );

        var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var finalUrl = $"{_endpoint}/{_bucketName}/{fileName}";

        return finalUrl;
    }

    public Task<Stream> DownloadAsync(string fileId)
    {
        throw new NotImplementedException("Download not implemented yet");
    }

    public Task DeleteAsync(string fileId)
    {
        throw new NotImplementedException("Delete not implemented yet");
    }
}
