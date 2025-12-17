using ExoticsCarsStoreServerSide.Domain.Models.ProductModule;
using ExoticsCarsStoreServerSide.Shared;

namespace ExoticsCarsStoreServerSide.Services.Specifications.ProductWithSpecifications
{
    public class ProductCountSpecifications : BaseSpecifications<Product,int>
    {
        public ProductCountSpecifications(ProductQueryParams queryParams) : base(ProductSpecificationsHelper.GetProductCriteria(queryParams))
        {
        }
    }
}
