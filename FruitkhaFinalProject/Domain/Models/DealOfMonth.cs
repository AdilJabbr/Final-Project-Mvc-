using Domain.Models.Common;

namespace Final_Project.Models
{
    public class DealOfMonth : BaseEntity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}
