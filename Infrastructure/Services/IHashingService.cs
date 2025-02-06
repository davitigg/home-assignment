namespace Infrastructure.Services
{
    public interface IHashingService
    {
        string GenerateHash(string deviceId, string pin);
        bool VerifyHash(string deviceId, string pin, string storedHash);
    }
}
