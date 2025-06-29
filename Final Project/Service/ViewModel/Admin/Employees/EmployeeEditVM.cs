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
        public int Id { get; set; }

        public string Name { get; set; }
        public string Position { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ImageUrlPath { get; set; } = null!;
        public IFormFile ImageUrl { get; set; } = null!;
    }
}
