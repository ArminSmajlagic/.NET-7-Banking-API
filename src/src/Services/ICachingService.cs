namespace src.Services
{
    public interface ICachingService
    {
        Task CacheRespons(string key, object data, TimeSpan ttl);
        Task<string> GetCachedRespons(string key);
        Task Remove(params string[] keys);
    }
}
