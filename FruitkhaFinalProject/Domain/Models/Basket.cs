using Domain.Models.Common;
using Final_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Basket : BaseEntity
    {
        public List<ProductsInBasket> BasketProducts { get; set; }
        public AppUser AppUser { get; set; } = null!;
        public string AppUserId { get; set; } = null!;
        public int Count { get; set; }
    }
}
