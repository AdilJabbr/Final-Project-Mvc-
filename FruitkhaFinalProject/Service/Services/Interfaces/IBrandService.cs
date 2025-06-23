using Service.ViewModel.Admin.Brands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IBrandService
    {
        Task CreateAsync(BrandCreateVM model);
        Task EditAsync(int? id, BrandEditVM model);
        Task DeleteAsync(int? id);
        Task<IEnumerable<BrandVM>> GetAllAsync();
        Task<BrandVM> GetByIdAsync(int? id);
    }
}
