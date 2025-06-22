using Domain.Models.Common;
using Final_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Position : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
