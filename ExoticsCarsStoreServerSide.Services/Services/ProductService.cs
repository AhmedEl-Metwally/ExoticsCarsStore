using AutoMapper;
using ExoticsCarsStoreServerSide.Domain.Models.ProductModule;
using ExoticsCarsStoreServerSide.Domain.Specifications;
using ExoticsCarsStoreServerSide.Services.Specifications.ProductWithSpecifications;
using ExoticsCarsStoreServerSide.ServicesAbstraction.Interface;
using ExoticsCarsStoreServerSide.Shared;
using ExoticsCarsStoreServerSide.Shared.CommonResult;
using ExoticsCarsStoreServerSide.Shared.DTOS.ProductDTOS;

namespace ExoticsCarsStoreServerSide.Services.Services
{
    public class ProductService(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductService
    {
        public async Task<PaginatedResult<ProductDTO>> GetAllProductsAsync(ProductQueryParams queryParams)
        {
            var RepositoryOfProducts = _unitOfWork.GetRepository<Product, int>();
            var specification = new ProductWithTypeAndBrandSpecification(queryParams);
            var Products = await RepositoryOfProducts.GetAllAsync(specification);
            var DataToReturn = _mapper.Map<IEnumerable<ProductDTO>>(Products);
            var CountOfReturnedData = DataToReturn.Count();
            var ProductCountSpecifications = new ProductCountSpecifications(queryParams);
            var CountOfAllProduct = await RepositoryOfProducts.CountAsync(ProductCountSpecifications);
            return new PaginatedResult<ProductDTO>(queryParams.pageIndex, CountOfReturnedData, CountOfAllProduct, DataToReturn);
        }

        public async Task<ErrorToReturnValue<ProductDTO>> GetProductByIdAsync(int id)
        {
            var specification = new ProductWithTypeAndBrandSpecification(id);
            var Products = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(specification);
            if (Products is null)
                return ValidationErrorToReturn.NotFound("Product.NotFound", $"Product with this Id:{id} is not found");
            return _mapper.Map<ProductDTO>(Products);
        }

        public async Task<IEnumerable<TypeDTO>> GetAllTypesAsync()
        {
            var Types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<TypeDTO>>(Types);
        }

        public async Task<IEnumerable<BrandDTO>> GetAllBrandsAsync()
        {
            var Brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<BrandDTO>>(Brands);
        }

    }
}
