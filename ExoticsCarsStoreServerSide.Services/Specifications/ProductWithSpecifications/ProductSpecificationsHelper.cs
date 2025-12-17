using ExoticsCarsStoreServerSide.Domain.Models.ProductModule;
using ExoticsCarsStoreServerSide.Shared;
using System.Linq.Expressions;

namespace ExoticsCarsStoreServerSide.Services.Specifications.ProductWithSpecifications
{
    public static class ProductSpecificationsHelper
    {
        public static Expression<Func<Product, bool>> GetProductCriteria(ProductQueryParams queryParams)
        {
            return P => (!queryParams.brandId.HasValue || P.BrandId == queryParams.brandId.Value)
                          && (!queryParams.typeId.HasValue || P.TypeId == queryParams.typeId.Value)
                          && (string.IsNullOrEmpty(queryParams.search) || P.Name.ToLower().Contains(queryParams.search.ToLower()));
        }
    }
}
