using AutoMapper;
using ExoticsCarsStoreServerSide.Domain.Contracts;
using ExoticsCarsStoreServerSide.Domain.Models.IdentityModule;
using ExoticsCarsStoreServerSide.Domain.Specifications;
using ExoticsCarsStoreServerSide.ServicesAbstraction.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace ExoticsCarsStoreServerSide.Services.Services
{
    public class ServiceManager(
                                    IUnitOfWork _unitOfWork,
                                    IMapper _mapper,
                                    IBasketRepository _basketRepository,
                                    ICacheRepository _cacheRepository,
                                    IConfiguration _configuration,
                                    UserManager<ApplicationUser> _userManager
                                ) : IServiceManager
    {
        private readonly Lazy<IProductService> _lazyProductService = new Lazy<IProductService>(() => new ProductService(_unitOfWork, _mapper));
        private readonly Lazy<IBasketService> _lazyBasketService = new Lazy<IBasketService>(() => new BasketService(_basketRepository, _mapper));
        private readonly Lazy<IOrderService> _lazyOrderService = new Lazy<IOrderService>(() => new OrderService(_mapper, _basketRepository, _unitOfWork));
        private readonly Lazy<ICacheService> _lazyCacheService = new Lazy<ICacheService>(() => new CacheService(_cacheRepository));
        private readonly Lazy<IAuthenticationService> _lazyAuthenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(_userManager, _configuration,_mapper));
        private readonly Lazy<IPaymentService> _lazyPaymentService = new Lazy<IPaymentService>(() => new PaymentService(_basketRepository,_unitOfWork, _configuration, _mapper));

        public IProductService ProductService => _lazyProductService.Value;

        public IBasketService BasketService => _lazyBasketService.Value;

        public IOrderService OrderService => _lazyOrderService.Value;

        public ICacheService CacheService => _lazyCacheService.Value;

        public IAuthenticationService AuthenticationService => _lazyAuthenticationService.Value;

        public IPaymentService PaymentService => _lazyPaymentService.Value;
    }
}
