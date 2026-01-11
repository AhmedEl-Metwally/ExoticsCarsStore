using ExoticsCarsStoreServerSide.Domain.Contracts;
using ExoticsCarsStoreServerSide.Domain.Specifications;
using ExoticsCarsStoreServerSide.Persistence.Data.DataSeed;
using ExoticsCarsStoreServerSide.Persistence.IdentityData.DataSeed;
using ExoticsCarsStoreServerSide.Persistence.Repository;
using ExoticsCarsStoreServerSide.Services.Mapping;
using ExoticsCarsStoreServerSide.Services.Resolvers;
using ExoticsCarsStoreServerSide.Services.Services;
using ExoticsCarsStoreServerSide.ServicesAbstraction.Interface;

namespace ExoticsCarsStoreServerSide.API.Extensions
{
    public static class CoreServicesExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection Services)
        {
            //builder.Services.AddAutoMapper(P => P.AddProfile<ProductProfile>());
            //builder.Services.AddAutoMapper(A =>A.LicenseKey ="",typeof(ProductProfile).Assembly);
            //14 AutoMapper
            Services.AddAutoMapper(Mapping =>
            {
                Mapping.AddProfile(new ProductProfile());
                Mapping.AddProfile(new BasketProfile());
                Mapping.AddProfile(new OrderProfile());
                Mapping.AddProfile(new AuthenticationProfile());
            });
            Services.AddTransient<ProductPictureUrlResolver>();
            Services.AddTransient<OrderItemPictureUrlResolver>();

            Services.AddScoped<IProductService, ProductService>();
            Services.AddScoped<IServiceManager, ServiceManager>();
            Services.AddScoped<IBasketService, BasketService>();
            Services.AddScoped<ICacheService, CacheService>();
            Services.AddScoped<IAuthenticationService, AuthenticationService>();
            Services.AddScoped<IOrderService, OrderService>();
            Services.AddScoped<IPaymentService, PaymentService>();

            Services.AddKeyedScoped<IDataInitializer, DataInitializer>("Default");
            Services.AddKeyedScoped<IDataInitializer, IdentityDataInitializer>("Identity");

            return Services;
        }
    }
}
