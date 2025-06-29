using AutoMapper;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repository;
using service.services.ınterfaces;
using Service.DTOs.Admin.Categories;
using Service.DTOs.Admin.Products;
using Service.Helpers.Exceptions;
using Service.Services.Interfaces;
using Service.ViewModel.Admin.Brands;

namespace Final_Project.Areas.Admin.Controllers
{
    
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper mapper;
        private readonly IBrandService _brandService;
        private readonly Db db;


        public ProductController(IProductService productService ,
                                 ICategoryService categoryService, 
                                 IBrandService brandService,
                                 IMapper _mapper,
                                 Db _db)
        {
            _productService = productService;
            _categoryService = categoryService;
            _brandService = brandService;
            mapper = _mapper;
            db = _db;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _productService.GetAllAsync());
        }
        public async Task<IActionResult> Create()
        {
            return View(await _productService.GetCreatedProductDto());

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateVM request)
        {
            var vm = await _productService.GetCreatedProductDto();
            request.Categories = vm.Categories;
            request.Brands = vm.Brands;


            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var result = await _productService.ProductCreate(request);

            if (!result.Success)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
                var categoriesAndBrands = await _productService.GetCreatedProductDto();
                request.Categories = categoriesAndBrands.Categories;
                request.Brands = categoriesAndBrands.Brands;

                return View(request);
            }

            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {

            return View(await _productService.GetProduct(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Delete( int id)
        {
            var isDeleted = await _productService.DeleteAsync(id);
            if (!isDeleted)
            {
                ModelState.AddModelError("", "Product dont delete");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            return View(await _productService.GetProductUpdateDto(id));
        }


        [HttpPost]
        public async Task<IActionResult> Edit(ProductEditVM request)
        {
            var vm = await _productService.GetProductUpdateDto(request.Id);
            request.Categories = vm.Categories;
            request.Brands = vm.Brands;

            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var result = await _productService.UpdateProductAsync(request);
            if (!result.Success)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
                return View(request);
            }

            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> GetPaginateDatas( int page = 1, int take = 9)
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

        //[HttpPost]
        //public async Task<IActionResult> AddImage(int productId, IFormFile uploadImage)
        //{

        //    if (uploadImage == null || uploadImage.Length == 0)
        //    {
        //        ModelState.AddModelError(string.Empty, "No file uploaded.");
        //        return BadRequest("No file uploaded.");
        //    }
        //    await _productService.AddImageAsync(productId, uploadImage);

        //    return RedirectToAction(nameof(Index));


        //}

        //[HttpPost]
        //public async Task<IActionResult> DeleteImage(int productId, int productImageId)
        //{
        //    await _productService.DeleteImageAsync(productId, productImageId);

        //    return RedirectToAction(nameof(Index));
       // }
    }

        
    
}