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
            CreateMap<OrderAddress, AddressDTO>().ReverseMap();
            CreateMap<DeliveryMethod, DeliveryMethodDTO>().ReverseMap();

            CreateMap<Order, OrderToReturnDTO>()
                .ForMember(dest => dest.DeliveryMethod,option => option.MapFrom(src=>src.DeliveryMethod.ShortName))
                .ForMember(D => D.DeliveryCost, O => O.MapFrom(S => S.DeliveryMethod.Price));



            CreateMap<OrderItem, OrderItemDTO>()
                .ForMember(dest => dest.ProductName,option => option.MapFrom(src =>src.Product.ProductName))
                .ForMember(dest => dest.ProductId, option => option.MapFrom(src => src.Product.ProductId))
                .ForMember(dest => dest.PictureUrl,option => option.MapFrom<OrderItemPictureUrlResolver>());
        }
    }
}
