using AutoMapper;
using ExoticsCarsStoreServerSide.Domain.Contracts;
using ExoticsCarsStoreServerSide.Domain.Models.ProductModule;
using ExoticsCarsStoreServerSide.ServicesAbstraction.Interface;
using ExoticsCarsStoreServerSide.Shared.DTOS.ProductDTOS;

namespace ExoticsCarsStoreServerSide.Services.Services
{
    public class ProductService(IUnitOfWork _unitOfWork,IMapper _mapper) : IProductService
    {
        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
        {
            var Products = await _unitOfWork.GetRepository<Product,int>().GetAllAsync();
            return _mapper.Map<IEnumerable<ProductDTO>>(Products);
        }

        public async Task<IEnumerable<ProductDTO>> GetProductByIdAsync(int id)
        {
            var Products = await _unitOfWork.GetRepository<Product,int>().GetByIdAsync(id);
            return _mapper.Map<IEnumerable<ProductDTO>>(Products);
        }

        public async Task<IEnumerable<TypeDTO>> GetAllTypesAsync()
        {
            var Types = await _unitOfWork.GetRepository<ProductType,int>().GetAllAsync();
            return _mapper.Map<IEnumerable<TypeDTO>>(Types);
        }

        public async Task<IEnumerable<BrandDTO>> GetAllBrandsAsync()
        {
            var Brands = await _unitOfWork.GetRepository<ProductBrand,int>().GetAllAsync();
            return _mapper.Map<IEnumerable<BrandDTO>>(Brands);
        }

    }
}
