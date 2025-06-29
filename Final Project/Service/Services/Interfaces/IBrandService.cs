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
        Task<bool> CreateAsync(BrandCreateVM vm);
        Task<bool> EditAsync(BrandEditVM vm);
        Task<bool> DeleteAsync(int id);
        Task<BrandEditVM> GetBrandEdit(int id);

        Task<IEnumerable<BrandVM>> GetAllAsync();
        Task<BrandVM> GetByIdAsync(int? id);
    }
}
