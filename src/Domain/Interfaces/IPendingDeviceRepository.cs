using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IPendingDeviceRepository
    {
        PendingDevice? Get(string key);
        void Set(string key, PendingDevice pendingDevice);
        void Remove(string key);
        List<PendingDevice> GetAll();
    }
}
