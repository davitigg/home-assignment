using System.Security.Cryptography;
using System.Text;
using Application.Common.Settings;
using Application.Interfaces;
using Microsoft.Extensions.Options;

namespace Application.Services
{
    public class HashingService(IOptions<HashingSettings> hashingSettings) : IHashingService
    {
        private readonly string _secretKey = hashingSettings.Value.SecretKey;

        public string GeneratePinHash(string deviceId, string pin)
        {
            if (string.IsNullOrWhiteSpace(deviceId)) throw new ArgumentNullException(nameof(deviceId));
            if (string.IsNullOrWhiteSpace(pin)) throw new ArgumentNullException(nameof(pin));

            byte[] deviceBytes = Encoding.UTF8.GetBytes(deviceId);
            byte[] pinBytes = Encoding.UTF8.GetBytes(pin);
            byte[] combinedBytes = CombineArrays(deviceBytes, pinBytes);
            return ComputeHmacSha256(combinedBytes);
        }

        public bool VerifyPinHash(string deviceId, string pin, string storedHash)
        {
            if (string.IsNullOrWhiteSpace(storedHash)) return false;

            string computedHash = GeneratePinHash(deviceId, pin);
            return string.Equals(computedHash, storedHash, StringComparison.OrdinalIgnoreCase);
        }

        public string GenerateBiometricHash(string deviceId, byte[] biometricData)
        {
            if (string.IsNullOrWhiteSpace(deviceId)) throw new ArgumentNullException(nameof(deviceId));
            if (biometricData == null || biometricData.Length == 0) throw new ArgumentNullException(nameof(biometricData));

            byte[] deviceBytes = Encoding.UTF8.GetBytes(deviceId);
            byte[] combinedBytes = CombineArrays(deviceBytes, biometricData);
            return ComputeHmacSha256(combinedBytes);
        }

        public bool VerifyBiometricHash(string deviceId, byte[] biometricData, string storedHash)
        {
            if (string.IsNullOrWhiteSpace(storedHash)) return false;

            string computedHash = GenerateBiometricHash(deviceId, biometricData);
            return string.Equals(computedHash, storedHash, StringComparison.OrdinalIgnoreCase);
        }

        private string ComputeHmacSha256(byte[] inputBytes)
        {
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_secretKey));
            byte[] hashBytes = hmac.ComputeHash(inputBytes);
            return Convert.ToHexString(hashBytes);
        }

        private byte[] CombineArrays(byte[] first, byte[] second)
        {
            byte[] combined = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, combined, 0, first.Length);
            Buffer.BlockCopy(second, 0, combined, first.Length, second.Length);
            return combined;
        }
    }
}
