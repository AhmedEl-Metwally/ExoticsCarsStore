namespace ExoticsCarsStoreServerSide.ServicesAbstraction.Interface
{
    public interface IServiceManager
    {
        public IProductService ProductService { get; }
        public IBasketService BasketService { get; }
        public IOrderService OrderService { get; }
        public ICacheService CacheService { get; }
        public IAuthenticationService AuthenticationService { get; }
    }
}
