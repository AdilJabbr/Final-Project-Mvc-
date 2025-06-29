using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Service.Helpers.Exceptions;
using Service.Services.Interfaces;
using Service.ViewModel.Admin.Brands;
using Service.ViewModel.Admin.News;

namespace Final_Project.Areas.Admin.Controllers
{
    public class NewsController : BaseController
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
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(NewsCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var result = await _newsService.CreateNews(request);
            if (!result.Success)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }

                return View(request);
            }
            return RedirectToAction("index");
        }
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _newsService.GetByIdAsync(id);

            if (category is null) return NotFound();

            return Ok(category);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            return View(await _newsService.NewsEditVM(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit( NewsEditVM request)
        {
            try
            {
                await _newsService.Edit(request);
                return RedirectToAction("index");
            }
            catch (ExistException ex)
            {
                return BadRequest(new { ErrorMessage = ex.Message });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { ErrorMessage = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();

            var isSuccess = await _newsService.Delete((int)id);

            return RedirectToAction("index");

        }
    }
}
