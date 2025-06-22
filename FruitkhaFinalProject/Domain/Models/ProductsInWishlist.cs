using Domain.Models.Common;
using Final_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ProductsInWishlist : BaseEntity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int WishlistId { get; set; }
        public Wishlist Wishlist { get; set; }

    }
}
