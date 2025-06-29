using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;

namespace Final_Project.Controllers
{
    public class AboutController : Controller
    {

        private readonly IEmployeeService employeeService;

        public AboutController(IEmployeeService _productService)
        {
            employeeService = _productService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await employeeService.GetAllAsync());
        }
    }
}
