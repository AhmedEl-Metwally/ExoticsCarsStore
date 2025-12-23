namespace ExoticsCarsStoreServerSide.ServicesAbstraction.Interface
{
    public interface ICacheService
    {
        Task<string?> GetCachedKeyAsync(string CacheKey);
        Task SetCacheAsync(string CacheKey, object CacheValue, TimeSpan TimeToLive);
    }
}
