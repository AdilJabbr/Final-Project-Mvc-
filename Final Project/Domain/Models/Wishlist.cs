using Domain.Models.Common;
using Final_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Wishlist : BaseEntity
    {
        public Product Product { get; set; } = null!;
        public int ProductId { get; set; }
        public AppUser AppUser { get; set; } = null!;
        public string AppUserId { get; set; } = null!;
    }


    public class WishlistCard
    {
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public decimal Price { get; set; }
    }
}
