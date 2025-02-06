namespace Infrastructure.Services
{
    public interface IMemoryCacheService
    {
        void Set<T>(string key, T value, TimeSpan? duration = null);
        T? Get<T>(string key);
        void Remove(string key);

    }
}