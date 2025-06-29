using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;
using Service.ViewModel.Admin.Contact;
using System.ComponentModel.DataAnnotations;

namespace Final_Project.Areas.Admin.Controllers
{
    public class ContactController : BaseController
    {      
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _contactService.GetAllAsync());
        }

        public async Task<IActionResult> Detail(int id)
        {
            return View(await _contactService.GetByIdAsync(id));
        }

        public async Task<IActionResult> Answer(int id)
        {
            return View(await _contactService.ContactCreateVMAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> Answer(ContactCreateVM dto)
        {
            var model = await _contactService.SendEmailContact(dto);
            return RedirectToAction("index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _contactService.DeleteAsync(id);
            return RedirectToAction("index");
        }

    }
}
