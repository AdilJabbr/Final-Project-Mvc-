using Service.ViewModel.Admin.Employees;
using Service.ViewModel.Admin.News;
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
        Task<(bool Success, List<string> Errors)> CreateEmployee(EmployeeCreateVM vm);

        Task<bool> Edit(EmployeeEditVM vm);
        Task<bool> Delete(int id);
        Task<EmployeeEditVM> EmployeeEditVM(int id);
        Task<IEnumerable<EmployeeVM>> GetAllAsync();
        Task<EmployeeVM> GetByIdAsync(int? id);
    }
}
