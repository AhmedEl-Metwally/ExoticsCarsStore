namespace ExoticsCarsStoreServerSide.Domain.Contracts
{
    public interface ICacheRepository
    {
        Task<string?> GetCachedKeyAsync(string CacheKey);
        Task SetCacheAsync(string CacheKey, string CacheValue, TimeSpan TimeToLive);
    }
}
