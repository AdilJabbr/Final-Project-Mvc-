using Domain.Models.Common;
using Final_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ProductsInBasket : BaseEntity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; } = 1;
        public int BasketId { get; set; }
        public Basket Basket { get; set; }
    }
}
