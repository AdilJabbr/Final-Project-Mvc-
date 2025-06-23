using Service.ViewModel.Admin.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IContactService
    {
        Task CreateAsync(ContactCreateVM model);
        Task DeleteAsync(int? id);
        Task<IEnumerable<ContactVM>> GetAllAsync();
        Task<ContactVM> GetByIdAsync(int? id);
    }
}
