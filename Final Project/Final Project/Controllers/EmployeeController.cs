using Microsoft.AspNetCore.Mvc;
using service.services.ınterfaces;
using Service.Services.Interfaces;

namespace Final_Project.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService employeeService;

        public EmployeeController(IEmployeeService _productService)
        {
            employeeService = _productService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await employeeService.GetAllAsync());
        }
    }
}
