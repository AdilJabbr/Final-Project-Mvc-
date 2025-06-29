using Final_Project.Areas.Admin.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace MVC_Mini_Project.Areas.Admin.Controllers
{
    public class DashboardController : BaseController
    {
        [Area("Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
