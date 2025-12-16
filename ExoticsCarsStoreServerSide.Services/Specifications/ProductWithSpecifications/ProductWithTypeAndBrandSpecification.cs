using ExoticsCarsStoreServerSide.Domain.Models.ProductModule;

namespace ExoticsCarsStoreServerSide.Services.Specifications.ProductWithSpecifications
{
    public class ProductWithTypeAndBrandSpecification : BaseSpecifications<Product,int>
    {
        // GetAllProduct
        public ProductWithTypeAndBrandSpecification(int? brandId, int? typeId) : base(
                                                                      P => (!brandId.HasValue || P.BrandId == brandId.Value)
                                                                                      && (!typeId.HasValue || P.TypeId ==typeId.Value)
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
