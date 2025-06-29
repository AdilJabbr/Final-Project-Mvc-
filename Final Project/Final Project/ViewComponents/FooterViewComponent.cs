using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;

namespace Final_Project.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly IBrandService _brandService;
        private readonly ISubscribeService _subscribeService;

        public FooterViewComponent(IBrandService brandService, ISubscribeService subscribeService)
        {
            _brandService = brandService;
            _subscribeService = subscribeService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var brands = await _brandService.GetAllAsync();
            return await Task.FromResult(View(brands));
        }
    }
}
