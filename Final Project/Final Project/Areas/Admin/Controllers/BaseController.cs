using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Final_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("Admin,SuperAdmin")]

    public class BaseController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
