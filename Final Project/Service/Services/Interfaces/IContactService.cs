using Service.DTOs.Admin.Categories;
using Service.ViewModel.Admin.Contact;
using Service.ViewModel.Admin.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IContactService
    {       
        Task<IEnumerable<ContactVM>> GetAllAsync();
        Task<ContactVM> GetByIdAsync(int? id);
        Task<bool> CreateAsync(ContactCreateVM vm);


        Task<ContactCreateVM> ContactCreateVMAsync(int id);
        Task<bool> SendEmailContact(ContactCreateVM dto);
        Task DeleteAsync(int? id);

    }
}
