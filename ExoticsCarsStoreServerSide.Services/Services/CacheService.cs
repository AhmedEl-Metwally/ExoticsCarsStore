using ExoticsCarsStoreServerSide.Domain.Contracts;
using ExoticsCarsStoreServerSide.ServicesAbstraction.Interface;
using System.Text.Json;

namespace ExoticsCarsStoreServerSide.Services.Services
{
    public class CacheService(ICacheRepository _cacheRepository) : ICacheService
    {
        public async Task<string?> GetCachedKeyAsync(string CacheKey) => await _cacheRepository.GetCachedKeyAsync(CacheKey);
     
        public async Task SetCacheAsync(string CacheKey, object CacheValue, TimeSpan TimeToLive)
        {
            var serializedCacheValue = JsonSerializer.Serialize(CacheValue,new JsonSerializerOptions() {PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            await _cacheRepository.SetCacheAsync(CacheKey, serializedCacheValue, TimeToLive);
        }
    }
}
