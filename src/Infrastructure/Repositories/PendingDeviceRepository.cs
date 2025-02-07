using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Repositories
{
    public class PendingDeviceRepository(MemoryCache memoryCache) : IPendingDeviceRepository
    {
        readonly MemoryCache _cache = memoryCache;

        public PendingDevice? Get(string key)
        {
            return _cache.TryGetValue<PendingDevice>(key, out var device) ? device : default;
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public void Set(string key, PendingDevice device)
        {
            _cache.Set(key, device, device.ExpirationDuration);
        }

        public List<PendingDevice> GetAll()
        {
            var cacheEntries = new List<PendingDevice>();

            var keys = _cache.Keys;
            foreach (var key in keys)
            {
                var entry = Get((string)key);

                if (entry != null)
                {
                    cacheEntries.Add(entry);
                }
            }

            return cacheEntries;
        }
    }
}