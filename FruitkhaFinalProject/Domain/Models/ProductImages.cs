using Domain.Models.Common;

namespace Final_Project.Models
{
    public class ProductImages : BaseEntity
    {
        public string Image { get; set; }
        public string Name { get; set; }
        public bool IsMain { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
