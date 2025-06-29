using Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Repository;
using service.services.ınterfaces;
using Service.DTOs.Admin.Products;
using Service.Services.Interfaces;
using Service.ViewModel.Admin.DealOfMonth;
using System.Reflection.Metadata;

namespace Final_Project.ViewComponents
{
    public class HomeOfferViewComponent : ViewComponent
    {
        private readonly IProductService _productService;
        public HomeOfferViewComponent(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var allProducts = await _productService.GetAllAsync();

            var cheapestProduct = allProducts
                .OrderBy(p => p.Price)
                .FirstOrDefault(); // Ən ucuz məhsul (yoxdursa null qaytaracaq)

            return View(cheapestProduct); // Model: ProductVM
        }
    }
}
