using Domain.Models.Common;

namespace Final_Project.Models
{
    public class Brands : BaseEntity
    {
        public string Name { get; set; }
        public int ProductId { get; set; }
        public ICollection<Product> Products { get; set; }
        public string Image {  get; set; }
    }
}
