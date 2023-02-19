using Microsoft.Extensions.Caching.Distributed;
using src.Services;
using System.Text.Json;

namespace src.Extensions
{
    public static class CachingRegistry
    {
        public static async Task SetRecordAsync<T>(this IDistributedCache cache,
                   string recordID,
                   T data,
                   TimeSpan? expireTime = null,
                   TimeSpan? unusedExpireTime = null)
        {
            var options = new DistributedCacheEntryOptions();

            options.AbsoluteExpirationRelativeToNow = expireTime ?? TimeSpan.FromSeconds(60);
            options.SlidingExpiration = unusedExpireTime ?? TimeSpan.FromSeconds(60);

            var jsonData = JsonSerializer.Serialize(data);

            await cache.SetStringAsync(recordID, jsonData, options);
        }

        public static async Task<string> GetRecordAsync(this IDistributedCache cache, string recordID)
        {
            var result = await cache.GetStringAsync(recordID);

            if (result == null)
            {
                return default!;
            }

            return result;
        }

        public static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(opts => {
                opts.Configuration = configuration.GetConnectionString("redisCaching");
                opts.InstanceName = "banking";
            });

            services.AddSingleton<ICachingService, RedisCachingService>();

            return services;
        }

    }
}
