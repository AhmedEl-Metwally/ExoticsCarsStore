using ExoticsCarsStoreServerSide.Domain.Models.ProductModule;
using ExoticsCarsStoreServerSide.Domain.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace AdminDashboard.Controllers
{
    public class ProductBrandsController(IUnitOfWork _unitOfWork) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            return View(brands);
        }

        public async Task<IActionResult> Create(ProductBrand brand)
        {
            try
            {
                await _unitOfWork.GetRepository<ProductBrand, int>().AddAsync(brand);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError("Name", "Please enter new name");
                return View("Index", await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync());
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetByIdAsync(id);
            _unitOfWork.GetRepository<ProductBrand, int>().Remove(brands);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
