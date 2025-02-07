namespace Application.Interfaces
{
    public interface IHashingService
    {
        public string GeneratePinHash(string deviceId, string pin);
        public bool VerifyPinHash(string deviceId, string pin, string storedHash);
        public string GenerateBiometricHash(string deviceId, byte[] biometricData);
        public bool VerifyBiometricHash(string deviceId, byte[] biometricData, string storedHash);

    }
}
