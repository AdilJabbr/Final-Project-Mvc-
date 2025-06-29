using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;

namespace Final_Project.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService categoryService)
        {
            _newsService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _newsService.GetAllAsync());

        }
        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var news = await _newsService.GetByIdAsync(id);

            if (news is null) return NotFound();

            return View(news);
        }
    }
}
