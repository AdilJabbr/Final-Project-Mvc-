using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;
using Service.ViewModel.Admin.Settings;

namespace Final_Project.Areas.Admin.Controllers
{
    public class SettingController : BaseController
    {
        private readonly ISettingService _settingService;

        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }
        public async Task<IActionResult> Index()
        {
            var settings = await _settingService.GetAllAsync();
            return View(settings);
        }

        public async Task<IActionResult> Create()
        {

            return View();
        }
        public async Task<IActionResult> Update(int id)
        {
            var updateDto = await _settingService.SettingEditVM(id);

            return View(updateDto);

        }
        [HttpPost]
        public async Task<IActionResult> Update(SettingEditVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            await _settingService.EditSettingAsync(vm);

            return RedirectToAction("index");
        }
    }
}
