using AutoMapper;
using ExoticsCarsStoreServerSide.Domain.Models.BasketModule;
using ExoticsCarsStoreServerSide.Shared.DTOS.BasketDTOS;

namespace ExoticsCarsStoreServerSide.Services.Mapping
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<CustomerBasket, BasketDTO>().ReverseMap();
            CreateMap<BasketItem, BasketItemDTO>().ReverseMap();
        }
    }
}
