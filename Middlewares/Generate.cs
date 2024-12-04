using System.Security.Cryptography;

namespace SmartHomeServer.Middlewares;

public static class Generate
{
    public static string GenerateSecretKey()
    {
        using (var hmac = new HMACSHA256())
        {
            var key = Convert.ToBase64String(hmac.Key);
            return key.Replace("+", "").Replace("/", "").Replace("=", "");
        }
    }
}
