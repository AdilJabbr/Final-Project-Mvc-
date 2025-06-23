using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModel.Admin.Employees
{
    public class EmployeeEditVM
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public string Description { get; set; }
        public string? Image { get; set; }
        public IFormFile UploadImage { get; set; }
    }
}
