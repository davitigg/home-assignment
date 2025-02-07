using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IDeviceRepository
    {
        Task AddAsync(Device device);
        Task UpdateAsync(Device device);
        Task<Device?> GetByUserIdAndDeviceIdAsync(int userId, string deviceId);
        Task<List<Device>> GetAllAsync();
    }
}
