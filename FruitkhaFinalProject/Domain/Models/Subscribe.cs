using Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Subscribe : BaseEntity
    {
        public string Email { get; set; } = null!;
        public DateTime SubscribedDate { get; set; }
    }
}
