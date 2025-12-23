using ExoticsCarsStoreServerSide.Domain.Contracts;
using StackExchange.Redis;

namespace ExoticsCarsStoreServerSide.Persistence.Repository
{
    public class CacheRepository : ICacheRepository
    {
        private readonly IDatabase _database;
        public CacheRepository(IConnectionMultiplexer connectionMultiplexer)
        {
            _database = connectionMultiplexer.GetDatabase();    
        }

        public async Task<string?> GetCachedKeyAsync(string CacheKey)
        {
            var cachedValue = await _database.StringGetAsync(CacheKey);
            return cachedValue.IsNullOrEmpty ? null : cachedValue.ToString();   
        }

        public async Task SetCacheAsync(string CacheKey, string CacheValue, TimeSpan TimeToLive)
            => await _database.StringSetAsync(CacheKey, CacheValue, TimeToLive);
      
    }
}
