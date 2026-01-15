using ExoticsCarsStoreServerSide.Domain.Models.ProductModule;
using ExoticsCarsStoreServerSide.Domain.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace AdminDashboard.Controllers
{
    public class ProductTypesController(IUnitOfWork _unitOfWork) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            return View(types);
        }

        public async Task<IActionResult> Create(ProductType type)
        {
            try
            {
                await _unitOfWork.GetRepository<ProductType, int>().AddAsync(type);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError("Name", "Please enter new name");
                return View("Index", await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync());
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            var types = await _unitOfWork.GetRepository<ProductType, int>().GetByIdAsync(id);
            _unitOfWork.GetRepository<ProductType, int>().Remove(types);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
