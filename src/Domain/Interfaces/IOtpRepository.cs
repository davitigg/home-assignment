using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IOtpRepository
    {
        OtpEntry? Get(string key);
        void Set(string key, OtpEntry otp);
        void Remove(string key);
        List<OtpEntry> GetAll();
    }
}