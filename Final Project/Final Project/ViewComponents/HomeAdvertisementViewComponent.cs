using Microsoft.AspNetCore.Mvc;

namespace Final_Project.ViewComponents
{
    public class HomeAdvertisementViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View());
        }
    }
}
