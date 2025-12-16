using ExoticsCarsStoreServerSide.Domain.Models.ProductModule;
using ExoticsCarsStoreServerSide.Shared;

namespace ExoticsCarsStoreServerSide.Services.Specifications.ProductWithSpecifications
{
    public class ProductWithTypeAndBrandSpecification : BaseSpecifications<Product,int>
    {
        // GetAllProduct
        public ProductWithTypeAndBrandSpecification(ProductQueryParams queryParams) : base(
          P => (!queryParams.brandId.HasValue || P.BrandId == queryParams.brandId.Value)
                          && (!queryParams.typeId.HasValue || P.TypeId == queryParams.typeId.Value)
                          &&(string.IsNullOrEmpty(queryParams.search) || P.Name.ToLower().Contains(queryParams.search.ToLower()))
                          )
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
        }

        // GetProductById
        public ProductWithTypeAndBrandSpecification(int id) : base(P => P.Id == id)
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
        }
    }
}
