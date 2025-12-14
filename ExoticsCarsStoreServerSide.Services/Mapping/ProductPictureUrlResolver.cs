using AutoMapper;
using ExoticsCarsStoreServerSide.Domain.Models.ProductModule;
using ExoticsCarsStoreServerSide.Shared.DTOS.ProductDTOS;
using Microsoft.Extensions.Configuration;

namespace ExoticsCarsStoreServerSide.Services.Mapping
{
    public class ProductPictureUrlResolver(IConfiguration _configuration) : IValueResolver<Product, ProductDTO, string>
    {
        public string Resolve(Product source, ProductDTO destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PictureUrl))
                return string.Empty;
            if (source.PictureUrl.StartsWith("http"))
                return source.PictureUrl;

            var baseUrl = _configuration.GetSection("URLS")["BaseURL"];
            if (string.IsNullOrEmpty(baseUrl))
                return string.Empty;

            var picUrl = $"{baseUrl}{source.PictureUrl}";
            return picUrl;


        }
    }
}

