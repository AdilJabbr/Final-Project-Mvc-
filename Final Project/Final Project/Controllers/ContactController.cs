using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;
using Service.ViewModel.Admin.Contact;

namespace Final_Project.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;

        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ContactCreateVM dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            await _contactService.CreateAsync(dto);
            return RedirectToAction("index", "Home");
        }
    }
}
