using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services
{
    public class HashingService(IConfiguration configuration) : IHashingService
    {
        private readonly string _secretKey = configuration["Hashing:SecretKey"] ?? throw new ArgumentNullException("SecretKey is not configured");

        public string GenerateHash(string deviceId, string pin)
        {
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_secretKey));
            string combinedString = $"{deviceId}:{pin}";
            byte[] hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(combinedString));
            return Convert.ToHexString(hashBytes);
        }

        public bool VerifyHash(string deviceId, string pin, string storedHash)
        {
            string computedHash = GenerateHash(deviceId, pin);
            return string.Equals(computedHash, storedHash, StringComparison.OrdinalIgnoreCase);
        }
    }
}
