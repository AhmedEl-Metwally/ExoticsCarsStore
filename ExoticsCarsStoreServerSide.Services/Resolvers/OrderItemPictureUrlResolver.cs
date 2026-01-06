using AutoMapper;
using ExoticsCarsStoreServerSide.Domain.Models.OrderModule;
using ExoticsCarsStoreServerSide.Shared.DTOS.OrderDTOS;
using Microsoft.Extensions.Configuration;

namespace ExoticsCarsStoreServerSide.Services.Resolvers
{
    public class OrderItemPictureUrlResolver(IConfiguration _configuration) : IValueResolver<OrderItem, OrderItemDTO, string>
    {
        public string Resolve(OrderItem source, OrderItemDTO destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.Product.PictureUrl))
                return string.Empty;
            if (source.Product.PictureUrl.StartsWith("http"))
                return source.Product.PictureUrl;

            var baseUrl = _configuration.GetSection("URLS")["BaseURL"];
            if (string.IsNullOrEmpty(baseUrl))
                return string.Empty;

            var picUrl = $"{baseUrl}{source.Product.PictureUrl}";
            return picUrl;
        }
    }
}
