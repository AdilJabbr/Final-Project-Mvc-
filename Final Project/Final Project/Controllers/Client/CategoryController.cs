using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Admin.Categories;
using Service.Helpers.Exceptions;
using Service.Services.Interfaces;

namespace Final_Project.Controllers.Client
{
   // [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _categoryService.GetAllAsync());

        }
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);

            if (category is null) return NotFound();

            return Ok(category);
        }
    }
}