using Domain.Models.Common;
using Final_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class SendNotfication : BaseEntity
    {
        public string UserId { get; set; } = null!;
        public string ProductId { get; set; } = null!;
        public AppUser? User { get; set; } = null!;
        public Product? Product { get; set; } = null!;

    }
}
