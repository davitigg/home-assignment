using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Services
{
    public class MemoryCacheService(IMemoryCache memoryCache) : IMemoryCacheService
    {
        private readonly IMemoryCache _memoryCache = memoryCache;
        private readonly TimeSpan _defaultCacheDuration = TimeSpan.FromMinutes(30);

        public void Set<T>(string key, T value, TimeSpan? duration = null)
        {
            _memoryCache.Set(key, value, duration ?? _defaultCacheDuration);
        }

        public T? Get<T>(string key)
        {
            return _memoryCache.TryGetValue(key, out T value) ? value : default;
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }
    }
}