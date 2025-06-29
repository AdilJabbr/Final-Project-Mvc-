using Microsoft.AspNetCore.Mvc;
using Service.Helpers.Exceptions;
using Service.Services;
using Service.Services.Interfaces;
using Service.ViewModel.Admin.Brands;
using Service.ViewModel.Admin.Employees;

namespace Final_Project.Areas.Admin.Controllers
{
    public class EmployeeController : BaseController
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _employeeService.GetAllAsync());

        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var result = await _employeeService.CreateEmployee(request);
            if (!result.Success)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }

                return View(request);
            }
            return RedirectToAction("index");
        }
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _employeeService.GetByIdAsync(id);

            if (category is null) return NotFound();

            return Ok(category);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            return View(await _employeeService.EmployeeEditVM(id));


        }

        [HttpPost]
        public async Task<IActionResult> Edit( EmployeeEditVM request)
        {
            try
            {
                await _employeeService.Edit(request);
                return RedirectToAction("index");
            }
            catch (ExistException ex)
            {
                return BadRequest(new { ErrorMessage = ex.Message });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { ErrorMessage = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();

            var isSuccess = await _employeeService.Delete((int)id);

            return RedirectToAction("index");
        }
    }
}
