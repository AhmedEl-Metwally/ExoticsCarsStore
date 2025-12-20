using ExoticsCarsStoreServerSide.Domain.Contracts;
using ExoticsCarsStoreServerSide.Domain.Models.BasketModule;
using StackExchange.Redis;
using System.Text.Json;

namespace ExoticsCarsStoreServerSide.Persistence.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer connectionMultiplexer)
        {
            _database = connectionMultiplexer.GetDatabase();
        }
        public async Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan timeToLive = default)
        {
            var JsonBasket = JsonSerializer.Serialize(basket);
            var IsCreateOrUpdate = await _database.StringSetAsync(basket.Id,JsonBasket,(timeToLive == default) ? TimeSpan.FromDays(7):timeToLive);
            if (IsCreateOrUpdate)
                return await GetBasketAsync(basket.Id);
            else
                return null;


        }

        public async Task<bool> DeleteBasketAsync(string basketId) => await _database.KeyDeleteAsync(basketId);
    
        public async Task<CustomerBasket?> GetBasketAsync(string basketId)
        {
            var Basket = await _database.StringGetAsync(basketId);           
            if (Basket.IsNullOrEmpty)
                return null;
            else
                return JsonSerializer.Deserialize<CustomerBasket?>(Basket!.ToString());
        }
    }
}
