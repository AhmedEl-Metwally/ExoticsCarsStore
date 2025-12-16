using AutoMapper;
using ExoticsCarsStoreServerSide.Domain.Models.ProductModule;
using ExoticsCarsStoreServerSide.Domain.Specifications;
using ExoticsCarsStoreServerSide.Services.Specifications.ProductWithSpecifications;
using ExoticsCarsStoreServerSide.ServicesAbstraction.Interface;
using ExoticsCarsStoreServerSide.Shared.DTOS.ProductDTOS;

namespace ExoticsCarsStoreServerSide.Services.Services
{
    public class ProductService(IUnitOfWork _unitOfWork,IMapper _mapper) : IProductService
    {
        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync(int? brandId, int? typeId)
        {
            var specification = new ProductWithTypeAndBrandSpecification(brandId, typeId);
            var Products = await _unitOfWork.GetRepository<Product,int>().GetAllAsync(specification);
            return _mapper.Map<IEnumerable<ProductDTO>>(Products);
        }

        public async Task<ProductDTO> GetProductByIdAsync(int id)
        {
            var specification = new ProductWithTypeAndBrandSpecification(id);
            var Products = await _unitOfWork.GetRepository<Product,int>().GetByIdAsync(specification);
            return _mapper.Map<ProductDTO>(Products);
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
