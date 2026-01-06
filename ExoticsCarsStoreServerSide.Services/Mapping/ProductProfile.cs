using AutoMapper;
using ExoticsCarsStoreServerSide.Domain.Models.ProductModule;
using ExoticsCarsStoreServerSide.Services.Resolvers;
using ExoticsCarsStoreServerSide.Shared.DTOS.ProductDTOS;

namespace ExoticsCarsStoreServerSide.Services.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductBrand,BrandDTO>();
            CreateMap<ProductType,TypeDTO>();

            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.ProductBrand, option => option.MapFrom(src => src.ProductBrand.Name))
                .ForMember(dest => dest.ProductType, option => option.MapFrom(src => src.ProductType.Name))
                .ForMember(dest => dest.PictureUrl,option =>option.MapFrom<ProductPictureUrlResolver>());
        }
    }
}
