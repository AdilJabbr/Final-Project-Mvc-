using Service.ViewModel.Admin.Employees;
using Service.ViewModel.Admin.Subscribes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<EmployeeVM> GetByIdAsync(int? id);
        Task<IEnumerable<EmployeeVM>> GetAllAsync();
        Task CreateAsync(EmployeeCreateVM entity);
        Task EditAsync(int? id, EmployeeEditVM model);
        Task DeleteAsync(int? id);
    }
}
