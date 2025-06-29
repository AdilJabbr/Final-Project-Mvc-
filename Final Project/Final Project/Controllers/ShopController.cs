using Microsoft.AspNetCore.Mvc;
using service.services.ınterfaces;
using Service.Services;
using Service.Services.Interfaces;

namespace Final_Project.Controllers
{
    public class ShopController : Controller
    {
        private readonly IShopService _shopService;
        private readonly IProductService _productService;

        public ShopController(IShopService shopService, IProductService productService)
        {
            _shopService = shopService;
            _productService = productService;
        }

        public IActionResult Shop()
        {
            return View();
        }
        public async Task<IActionResult> Index(string? search,
        int? sort,
        List<int>? categoryIds,
        List<int>? brandIds,
        int? maxPrice,
        int? minPrice,
        int pageRow = 1)
        {
            var shop = await _shopService.GetShop(pageRow, search, sort, categoryIds, brandIds, maxPrice, minPrice);
            return View(shop);
        }

        [HttpPost]
        public IActionResult Search(string search)
        {
            return RedirectToAction("Index", new { search });
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var news = await _productService.GetByIdAsync(id);

            if (news is null) return NotFound();

            return View(news);
        }
    }
}
