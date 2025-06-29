using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;
using Service.ViewModel.Admin.Subscribes;

namespace Final_Project.Areas.Admin.Controllers
{
    public class SubscribeController : BaseController
    {
        //private readonly ISubscribeService _subscribeService;

        //public SubscribeController(ISubscribeService subscribeService)
        //{
        //    _subscribeService = subscribeService;
        //}

        //public async Task<IActionResult> Index()
        //{
        //    var subscribe = await _subscribeService.GetAllAsync();
        //    return View(subscribe);
        //}


        private readonly ISubscribeService _subscribeService;

        public SubscribeController(ISubscribeService subscribeService)
        {
            _subscribeService = subscribeService;
        }

        public async Task<IActionResult> Index()
        {
            var newsletters = await _subscribeService.GetAllAsync();

            var viewModel = new SubsribeListVM
            {
                Items = newsletters.Select(n => new SubscribeVM
                {
                    Id = n.Id,
                    Email = n.Email,
                    CreateDate = n.CreatedDate
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _subscribeService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
