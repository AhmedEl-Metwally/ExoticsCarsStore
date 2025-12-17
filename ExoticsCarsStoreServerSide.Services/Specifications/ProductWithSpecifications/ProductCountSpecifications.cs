using ExoticsCarsStoreServerSide.Domain.Models.ProductModule;
using ExoticsCarsStoreServerSide.Shared;

namespace ExoticsCarsStoreServerSide.Services.Specifications.ProductWithSpecifications
{
    public class ProductCountSpecifications : BaseSpecifications<Product,int>
    {
        public ProductCountSpecifications(ProductQueryParams queryParams) : base(
          P => (!queryParams.brandId.HasValue || P.BrandId == queryParams.brandId.Value)
                          && (!queryParams.typeId.HasValue || P.TypeId == queryParams.typeId.Value)
                          && (string.IsNullOrEmpty(queryParams.search) || P.Name.ToLower().Contains(queryParams.search.ToLower())))
        {
        }
    }
}
