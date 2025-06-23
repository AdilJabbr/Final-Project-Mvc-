using Domain.Models;
using Service.ViewModel.Admin.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface ISettingService 
    {
        Task CreateAsync(SettingCreateVM model);
        Task EditAsync(int? id, SettingEditVM model);
        Task DeleteAsync(int? id);
        Task<IEnumerable<SettingVM>> GetAllAsync();
        Task<SettingVM> GetByIdAsync(int? id);

    }
}
