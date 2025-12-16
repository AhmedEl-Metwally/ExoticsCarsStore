using ExoticsCarsStoreServerSide.Shared.DTOS.ProductDTOS;

namespace ExoticsCarsStoreServerSide.ServicesAbstraction.Interface
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetAllProductsAsync(int? brandId, int? typeId);
        Task<ProductDTO> GetProductByIdAsync(int id);
        Task<IEnumerable<BrandDTO>> GetAllBrandsAsync();
        Task<IEnumerable<TypeDTO>> GetAllTypesAsync();
    }
}
