using AutoMapper;
using ExoticsCarsStoreServerSide.Domain.Models.OrderModule;
using ExoticsCarsStoreServerSide.Services.Resolvers;
using ExoticsCarsStoreServerSide.Shared.DTOS.OrderDTOS;

namespace ExoticsCarsStoreServerSide.Services.Mapping
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<AddressDTO, OrderAddress>();
            CreateMap<Order, OrderToReturnDTO>()
                .ForMember(dest => dest.DeliveryMethod,option => option.MapFrom(src=>src.DeliveryMethod.ShortName));

            CreateMap<OrderItem, OrderItemDTO>()
                .ForMember(dest => dest.ProductName,option => option.MapFrom(src =>src.Product.ProductName))
                .ForMember(dest => dest.PictureUrl,option => option.MapFrom<OrderItemPictureUrlResolver>());
        }
    }
}
