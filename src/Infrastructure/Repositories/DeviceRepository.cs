using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class DeviceRepository(ApplicationDbContext context) : IDeviceRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task AddAsync(Device device)
        {
            await _context.AddAsync(device);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Device device)
        {
            _context.Set<Device>().Attach(device);
            _context.Entry(device).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<Device?> GetByUserIdAndDeviceIdAsync(int userId, string deviceId)
        {
            return await _context.Set<Device>().AsNoTracking().FirstOrDefaultAsync(d => d.UserId == userId && d.DeviceId == deviceId);
        }

        public async Task<List<Device>> GetAllAsync()
        {
            return await _context.Set<Device>().AsNoTracking().ToListAsync();
        }
    }
}
