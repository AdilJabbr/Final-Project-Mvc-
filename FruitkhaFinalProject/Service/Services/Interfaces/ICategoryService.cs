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
        Task CreateAsync(CategoryCreateVM model);
        Task EditAsync(int? id, CategoryEditVM model);
        Task DeleteAsync(int? id);
        Task<IEnumerable<CategoryVM>> GetAllAsync();
        Task<CategoryVM> GetByIdAsync(int? id);
    }
}
