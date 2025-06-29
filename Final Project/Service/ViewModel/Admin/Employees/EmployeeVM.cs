using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModel.Admin.Employees
{
    public class EmployeeVM
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Position { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
    }
}
