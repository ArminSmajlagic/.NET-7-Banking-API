using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Moq;
using src.Services;

namespace banking.test.Redis
{
    public class CachingServiceTest
    {
        private readonly IDistributedCache _cache;
        private readonly ICachingService _cachingServiceUnderTest;
        public CachingServiceTest()
        {
            var opts = Options.Create(new MemoryDistributedCacheOptions());
            _cache = new MemoryDistributedCache(opts);
            _cachingServiceUnderTest = new RedisCachingService(_cache);
        }

        [Fact]
        public async void GetCachedRespons_ShouldReturnCachedValue_WhenValidKeyIsProvided()
        {
            //Arrange
            var key = "test_id";
            var value = "test_value";
            var ttl = TimeSpan.FromSeconds(10);
            //Act
            await _cachingServiceUnderTest.CacheRespons(key, value, ttl);
            var respons = await _cachingServiceUnderTest.GetCachedRespons(key);
            //Assert
            Assert.NotNull(respons);
            Assert.Contains("test_value", respons);
        }

        [Fact]
        public async void GetCachedRespons_ShouldNotBeThere_WhenItExpires()
        {   
            //Arrange
            var key = "test_id";
            var value = "test_value";
            var ttl = TimeSpan.FromMilliseconds(5000);
            //Act
            await _cachingServiceUnderTest.CacheRespons(key, value, ttl);
            await Task.Delay(5000);
            var respons = await _cachingServiceUnderTest.GetCachedRespons("test_id");
            //Assert
            Assert.DoesNotContain("test_value", respons);
        }

        [Fact]
        public async void Remove_ShouldRemoveExistingItemInCache_WhenExistingKeyIsProvided()
        {
            //Arrange
            var key = "test_id";
            var value = "test_value";
            var ttl = TimeSpan.FromSeconds(60);
            //Act 1
            await _cachingServiceUnderTest.CacheRespons(key, value, ttl);
            var respons = await _cachingServiceUnderTest.GetCachedRespons("test_id");
            //Assert 1
            Assert.Contains("test_value", respons);
            //Act 2
            await _cachingServiceUnderTest.Remove(key);
            respons = await _cachingServiceUnderTest.GetCachedRespons("test_id");
            //Assert 2
            Assert.DoesNotContain("test_value", respons);

        }
    }
}
