using ExoticsCarsStoreServerSide.Presentation.Attributes;
using ExoticsCarsStoreServerSide.ServicesAbstraction.Interface;
using ExoticsCarsStoreServerSide.Shared;
using ExoticsCarsStoreServerSide.Shared.DTOS.ProductDTOS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExoticsCarsStoreServerSide.API.Controllers
{

    public class ProductsController(IServiceManager _serviceManager) : ApiBaseController
    {
        [HttpGet]
        [Cache]
        [Authorize]
        public async Task<ActionResult<PaginatedResult<ProductDTO>>> GetAllProductsAsync([FromQuery] ProductQueryParams queryParams)
        {
            var Products = await _serviceManager.ProductService.GetAllProductsAsync(queryParams);
            return Ok(Products);
        }

        [HttpGet("Id")]
        public async Task<ActionResult<ProductDTO>> GetProductByIdAsync(int id)
        {
            var ResultOfProducts = await _serviceManager.ProductService.GetProductByIdAsync(id);
            return HandleResult<ProductDTO>(ResultOfProducts);
        }

        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<TypeDTO>>> GetAllTypesAsync()
        {
            var Types = await _serviceManager.ProductService.GetAllTypesAsync();
            return Ok(Types);
        }

        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<BrandDTO>>> GetAllBrandsAsync()
        {
            var Brands = await _serviceManager.ProductService.GetAllBrandsAsync();
            return Ok(Brands);
        }
    }
}


