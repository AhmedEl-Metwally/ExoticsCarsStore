using ExoticsCarsStoreServerSide.Shared.DTOS.BasketDTOS;

namespace ExoticsCarsStoreServerSide.ServicesAbstraction.Interface
{
    public interface IBasketService
    {
        Task<BasketDTO> GetBasketAsync(string id);
        Task<BasketDTO> CreateOrUpdateBasketAsync(BasketDTO basket);
        Task<bool> DeleteBasketAsync(string id);

    }
}
