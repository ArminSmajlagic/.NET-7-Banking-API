using Microsoft.Extensions.Caching.Distributed;
using src.Extensions;

namespace src.Services
{
    public class RedisCachingService : ICachingService
    {
        private readonly IDistributedCache _cache;

        public RedisCachingService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task CacheRespons(string key, object data, TimeSpan ttl)
        {
            if(string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }

            await _cache.SetRecordAsync(key, data, ttl);
        }

        public async Task<string> GetCachedRespons(string key)
        {
            var respons = await _cache.GetRecordAsync(key);

            return respons;
        }

        public async Task Remove(params string[] keys)
        {
            foreach(var key in keys)
            {
                await _cache.RemoveAsync(key);
            }
        }
    }
}
