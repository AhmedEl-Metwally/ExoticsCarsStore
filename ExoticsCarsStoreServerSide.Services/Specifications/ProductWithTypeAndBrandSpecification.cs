using ExoticsCarsStoreServerSide.Domain.Models.ProductModule;

namespace ExoticsCarsStoreServerSide.Services.Specifications
{
    public class ProductWithTypeAndBrandSpecification : BaseSpecifications<Product,int>
    {
        public ProductWithTypeAndBrandSpecification() : base()
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
        }
    }
}
