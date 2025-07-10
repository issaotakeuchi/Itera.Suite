using System.Security.Cryptography;
using System.Text;

namespace Itera.Suite.Infrastructure.Helpers;

public static class AwsV4Signer
{
    public static byte[] HmacSHA256(string data, byte[] key)
    {
        using var hmac = new HMACSHA256(key);
        return hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
    }

    public static string Hash(byte[] payload)
    {
        using var sha256 = SHA256.Create();
        return BitConverter.ToString(sha256.ComputeHash(payload)).Replace("-", "").ToLower().Replace("-", "");
    }
}
