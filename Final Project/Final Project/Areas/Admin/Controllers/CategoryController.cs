using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Admin.Categories;
using Service.Helpers.Exceptions;
using Service.Services.Interfaces;

namespace Final_Project.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
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
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create( CategoryCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var isCreated = await _categoryService.CreateAsync(request);

            return RedirectToAction("Index");
            
        }
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);

            if (category is null) return NotFound();

            return Ok(category);
        }
      
       [HttpGet]
       public async Task<IActionResult> Edit (int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category is null) return NotFound();

            return View(await _categoryService.GetCategoryEdit(id));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryEditVM request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(request);
                }
                await _categoryService.EditAsync(request);

                return RedirectToAction("Index");
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
        public async Task<IActionResult> Delete( int id)
        {
            var isDeleted = await _categoryService.DeleteAsync(id);
            if (!isDeleted)
            {
                ModelState.AddModelError("", "Category dont delete");
            }

            return RedirectToAction("Index");         
        }
    }
}
