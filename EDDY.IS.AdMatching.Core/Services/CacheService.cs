using EDDY.IS.AdMatching.Domain.Services.Interfaces;
using EDDY.IS.Common.Settings;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace EDDY.IS.AdMatching.Core.Services
{
    public class CacheService : ICacheService
    {
        private readonly ILogger<CacheService> _log;
        private readonly RedisSettings _redisSettings;
        private readonly IDistributedCache _redisCache;
        private readonly IMemoryCache _memoryCache;
        private readonly IDatabase _redis;

        private static SemaphoreSlim _semaphore = new SemaphoreSlim(3);
        private static SemaphoreSlim _semaphoreTTL = new SemaphoreSlim(2);

        public CacheService(
            ILogger<CacheService> log,
            IOptions<RedisSettings> redisSettings,
            IDistributedCache redisCache,
            IMemoryCache memoryCache,
            IConnectionMultiplexer redis
            )
        {
            _log = log;
            _redisCache = redisCache;
            _redisSettings = redisSettings.Value;
            _memoryCache = memoryCache;
            _redis = redis.GetDatabase();
        }

        private string buildCacheKey(string key) => $"{_redisSettings.CachePrefix}:AMS:{key}";

        /// <summary>
        /// If the key has 20% or less of TTL will return true
        /// </summary>
        /// <param name="key">cache key</param>
        /// <returns>if the key needs to be recomputed</returns>
        public async Task<bool> NeedsReCompute(string key) {

            try
            {
                await _semaphoreTTL.WaitAsync();
                var cacheKey = buildCacheKey(key);

                var ttl = await _redis.KeyTimeToLiveAsync(cacheKey);
                if (ttl == null || !ttl.HasValue) return true;

                var offset = _redisSettings.LifeSpanSeconds * 0.20;
                return ttl.Value.TotalSeconds <= offset;

            }
            catch (Exception ex)
            {
                _log.LogError(ex, "Error retrieving TTL from cache - {key}", key);
                return true;
            }
            finally
            {
                _semaphoreTTL.Release();
            }
        }

        public async Task<TEntity> RefreshFromDistributedCache<TEntity>(string key) {
            try
            {
                await _semaphore.WaitAsync();
                var cacheKey = buildCacheKey(key);

                var value = await GetFromRedis<TEntity>(cacheKey);
                AddMemCache(cacheKey, value);

                return value;
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "Error retrieving TTL from cache - {key}", key);
                return default(TEntity);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task<TEntity> GetFromCache<TEntity>(string key)
        {
            try
            {
                
                var cacheKey = buildCacheKey(key);

                var cacheMem = _memoryCache.Get<TEntity>(cacheKey);
                if (cacheMem != null) return cacheMem;

                await _semaphore.WaitAsync();
                var value = await GetFromRedis<TEntity>(cacheKey);
                AddMemCache(cacheKey, value);

                return value;
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "Error retrieving value from cache - {key}", key);
                return default(TEntity);
            }
            finally {
                _semaphore.Release();
            }
        }

        public async Task<bool> SetValueToCache<TEntity>(string key, TEntity value)
        {
            try
            {
                await _semaphore.WaitAsync();
                var expiration = DateTime.Now.AddSeconds(_redisSettings.LifeSpanSeconds);
                var cacheKey = buildCacheKey(key);

                AddMemCache(cacheKey, value);

                if (await NeedsReCompute(key))
                {
                    var serializedValue = JsonSerializer.Serialize(value, new JsonSerializerOptions() { ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles });
                    await _redisCache.SetStringAsync(cacheKey, serializedValue, new DistributedCacheEntryOptions
                    {
                        AbsoluteExpiration = expiration
                    });
                }

                return true;
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "Error adding value to cache - {key}", key);
                return false;
            }
            finally
            {
                _semaphore.Release();
            }
        }

        private void AddMemCache<TEntity>(string key, TEntity value)
        {
            var cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(DateTime.Now.AddSeconds(_redisSettings.LifeSpanSeconds));

            _memoryCache.Set(key, value, cacheOptions);
        }

        private async Task<TEntity> GetFromRedis<TEntity>(string key) {
            var cacheValue = await _redisCache.GetStringAsync(key);
            if (string.IsNullOrEmpty(cacheValue))
                return default(TEntity);

            var value = JsonSerializer.Deserialize<TEntity>(cacheValue);
            return value;
        }
    }
}
