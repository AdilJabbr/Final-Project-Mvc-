using Domain.Models.Common;

namespace Final_Project.Models
{
    public class Brands : BaseEntity
    {
        public string Name { get; set; } = null!;
        public ICollection<Product> Products { get; set; } = [];
        public string ImageUrl {  get; set; } = null!;
    }
}
