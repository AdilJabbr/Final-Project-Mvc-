using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;

namespace Final_Project.Controllers.Client
{
    public class WishlistController : Controller
    {
        private readonly IWishlistService _wishlistService;

        public WishlistController(IWishlistService wishlistService)
        {
            _wishlistService = wishlistService;
        }


        public async Task<IActionResult> Index()
        {
            var dto = await _wishlistService.WishListCardVM();
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> AddToWishList(int id)
        {
            await _wishlistService.AddToWishListAsync(id);
            return RedirectToAction("index");

        }

        public async Task<IActionResult> WishlistCount()
        {
            var count = await _wishlistService.WishlistCount();
            return Json(new { count = count });
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _wishlistService.AddToWishListAsync(id);
            return Ok();
        }
    }
}
