using Domain.Models.Common;

namespace Final_Project.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = null!;
        public int ProductId { get; set; } 
        public ICollection<Product> Products { get; set; } = [];

    }
}
