using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Service.Helpers.Exceptions;
using Service.Services;
using Service.Services.Interfaces;
using Service.ViewModel.Admin.Brands;

namespace Final_Project.Areas.Admin.Controllers
{
    public class BrandsController : BaseController
    {
        private readonly IBrandService _brandService;

        public BrandsController(IBrandService brandService)
        {
            _brandService = brandService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _brandService.GetAllAsync());

        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(BrandCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var isCreated = await _brandService.CreateAsync(request);

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _brandService.GetByIdAsync(id);

            if (category is null) return NotFound();

            return Ok(category);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            return View(await _brandService.GetBrandEdit(id));

        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, BrandEditVM request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(request);
                }
                await _brandService.EditAsync(request);

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
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _brandService.DeleteAsync(id);
            if (!isDeleted)
            {
                ModelState.AddModelError("", "Brand dont delete");
            }

            return RedirectToAction("Index");
        }

    }
}
