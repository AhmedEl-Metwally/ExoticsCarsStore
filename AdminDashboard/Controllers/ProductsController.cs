using AdminDashboard.Helpers;
using AdminDashboard.ViewModels.ProductViewModels;
using AutoMapper;
using ExoticsCarsStoreServerSide.Domain.Models.ProductModule;
using ExoticsCarsStoreServerSide.Domain.Specifications;
using ExoticsCarsStoreServerSide.Services.Specifications.ProductWithSpecifications;
using ExoticsCarsStoreServerSide.Shared;
using Microsoft.AspNetCore.Mvc;

namespace AdminDashboard.Controllers
{
    public class ProductsController(IUnitOfWork _unitOfWork, IMapper _mapper) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var queryParams = new ProductQueryParams();
            var productRepo = _unitOfWork.GetRepository<Product, int>();
            var specification = new ProductWithTypeAndBrandSpecification(queryParams, true);
            var products = await productRepo.GetAllAsync(specification);
            var productViewModel = products.Select(product => new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                PictureUrl = product.PictureUrl,
                Price = product.Price,
                BrandId = product.BrandId,
                TypeId = product.TypeId,
                Brand = product.ProductBrand,
                Type = product.ProductType
            });

            return View(productViewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Image is not null)
                    model.PictureUrl = PictureSettings.UploadFile(model.Image, "products");

                var mappedProduct = _mapper.Map<ProductViewModel, Product>(model);
                var productRepo = _unitOfWork.GetRepository<Product, int>();
                await productRepo.AddAsync(mappedProduct);
                await _unitOfWork.SaveChangesAsync();
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(id);
            var mappedProduct = _mapper.Map<Product, ProductViewModel>(product);
            return View(mappedProduct);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductViewModel model)
        {
            if (id != model.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                if (model.Image != null)
                    PictureSettings.DeleteFile(model.PictureUrl, "products");
                model.PictureUrl = PictureSettings.UploadFile(model.Image, "products");

                var mappedProduct = _mapper.Map<Product>(model);
                _unitOfWork.GetRepository<Product, int>().Update(mappedProduct);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(id);
            if (product is null)
                return NotFound();
            var mappedProduct = _mapper.Map<Product, ProductViewModel>(product);
            return View(mappedProduct);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, ProductViewModel model) 
        {
            if (id != model.Id)
                return BadRequest();

            try
            {
                var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(id);
                if (product is null)
                    return NotFound();
                if (product.PictureUrl is not null)
                    PictureSettings.DeleteFile(product.PictureUrl, "products");

                _unitOfWork.GetRepository<Product, int>().Remove(product);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return View(model);;
            }
        }

    }
}
