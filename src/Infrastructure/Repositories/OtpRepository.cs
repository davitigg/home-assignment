using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Repositories
{
    public class OtpRepository(MemoryCache memoryCache) : IOtpRepository
    {
        readonly MemoryCache _cache = memoryCache;

        public OtpEntry? Get(string key)
        {
            return _cache.TryGetValue<OtpEntry>(key, out var otp) ? otp : default;
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public void Set(string key, OtpEntry otp)
        {
            _cache.Set(key, otp, otp.ExpirationDuration);
        }

        public List<OtpEntry> GetAll()
        {
            var cacheEntries = new List<OtpEntry>();

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