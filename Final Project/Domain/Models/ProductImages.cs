using Domain.Models.Common;

namespace Final_Project.Models
{
    public class ProductImages : BaseEntity
    {
        public string ImageUrl { get; set; } = null!;
        public int ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
