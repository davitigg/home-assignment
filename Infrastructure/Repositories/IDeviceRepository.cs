using Domain.Entities;

namespace Infrastructure.Repositories
{
    public interface IDeviceRepository
    {
        Task AddOrUpdateDeviceAsync(Device device);
        Task<List<Device>> GetAllDevicesAsync();
    }
}