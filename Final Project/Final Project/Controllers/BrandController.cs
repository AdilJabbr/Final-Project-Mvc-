using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;

namespace Final_Project.Controllers
{
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService categoryService)
        {
            _brandService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _brandService.GetAllAsync());

        }
    }
}
