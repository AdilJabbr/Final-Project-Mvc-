using Microsoft.AspNetCore.Mvc;
using service.services.ınterfaces;

namespace Final_Project.Controllers.Client
{
    public class ShopController : Controller
    {
        private readonly IProductService _productService;

        public ShopController(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _productService.GetAllAsync());
        }

        [HttpGet]
        public async Task<IActionResult> ProductDetail(int id)
        {
            var product = await _productService.DetailAsync(id);

            if (product is null) return NotFound();

            return View(product);
        }
        [HttpGet]
        public async Task<IActionResult> GetPaginateDatas(int page = 1, int take = 9)
        {
            return View(await _productService.GetPaginateAsync(page, take));
        }




        [HttpGet]
        public async Task<IActionResult> SortByPriceAscending()
        {
            return View(await _productService.SortByPriceAscending());
        }

        [HttpGet]
        public async Task<IActionResult> SortByPriceDescending()
        {
            return View(await _productService.SortByPriceDescending());
        }
    }
}
