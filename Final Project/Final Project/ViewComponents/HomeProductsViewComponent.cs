using Microsoft.AspNetCore.Mvc;
using service.services.ınterfaces;

namespace Final_Project.ViewComponents
{
    public class HomeProductsViewComponent : ViewComponent
    {
        private readonly IProductService _productService;
        public HomeProductsViewComponent(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View(await _productService.GetAllAsync()));
        }
    }
}
