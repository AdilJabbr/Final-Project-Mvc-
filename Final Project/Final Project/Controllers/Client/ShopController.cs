using Microsoft.AspNetCore.Mvc;
using service.services.ınterfaces;
using Service.Services.Interfaces;

namespace Final_Project.Controllers.Client
{
    public class ShopController : Controller
    {
        private readonly IShopService _shopService;

        public ShopController(IShopService shopService)
        {
            _shopService = shopService;
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
    }
}
