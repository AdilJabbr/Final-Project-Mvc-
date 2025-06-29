using Service.DTOs.Admin.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<bool> CreateAsync(CategoryCreateVM vm);
        Task<bool> EditAsync(CategoryEditVM vm);
        Task<bool> DeleteAsync(int id);
        Task<CategoryEditVM> GetCategoryEdit(int id);
        Task<IEnumerable<CategoryVM>> GetAllAsync();
        Task<CategoryVM> GetByIdAsync(int? id);
    }
}
