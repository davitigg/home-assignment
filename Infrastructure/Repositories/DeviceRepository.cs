using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class DeviceRepository(ApplicationDbContext context) : IDeviceRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task AddOrUpdateDeviceAsync(Device device)
        {
            var existingDevice = await _context.Set<Device>().FirstOrDefaultAsync(d => d.DeviceId == device.DeviceId);

            if (existingDevice != null)
            {
                _context.Set<Device>().Update(existingDevice);
            }
            else
            {
                await _context.Set<Device>().AddAsync(device);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<List<Device>> GetAllDevicesAsync()
        {
            return await _context.Set<Device>().AsNoTracking().ToListAsync();
        }
    }
}
