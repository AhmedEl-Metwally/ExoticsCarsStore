using AutoMapper;
using ExoticsCarsStoreServerSide.Domain.Contracts;
using ExoticsCarsStoreServerSide.Domain.Exceptions.NotFoundExceptions;
using ExoticsCarsStoreServerSide.Domain.Models.BasketModule;
using ExoticsCarsStoreServerSide.ServicesAbstraction.Interface;
using ExoticsCarsStoreServerSide.Shared.DTOS.BasketDTOS;

namespace ExoticsCarsStoreServerSide.Services.Services
{
    public class BasketService(IBasketRepository _basketRepository,IMapper _mapper) : IBasketService
    {
        public async Task<BasketDTO> CreateOrUpdateBasketAsync(BasketDTO basket)
        {
            var CustomerBaskets = _mapper.Map<BasketDTO,CustomerBasket>(basket);
            var CreateOrUpdateBasket = await _basketRepository.CreateOrUpdateBasketAsync(CustomerBaskets);
            return _mapper.Map<CustomerBasket,BasketDTO>(CreateOrUpdateBasket!);
        }

        public async Task<bool> DeleteBasketAsync(string id) => await _basketRepository.DeleteBasketAsync(id);

        public async Task<BasketDTO> GetBasketAsync(string id)
        {
            var Basket = await _basketRepository.GetBasketAsync(id);
            if (Basket is null)
                throw new BasketNotFoundException(id);
            return _mapper.Map<CustomerBasket,BasketDTO>(Basket);
        }
    }
}
