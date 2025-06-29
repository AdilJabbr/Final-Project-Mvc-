using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModel.Admin.Employees
{
    public class EmployeeCreateVM
    {
        public string Name { get; set; } = null!;
        public string Position { get; set; } = null!;
        public string Description { get; set; } = null!;
        public IFormFile ImageUrl { get; set; } = null!;

    }
}
