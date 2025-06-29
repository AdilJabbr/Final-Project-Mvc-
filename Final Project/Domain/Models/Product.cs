using Domain.Models;
using Domain.Models.Common;

namespace Final_Project.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public string Description { get; set; }

        public string IsMainPicture { get; set; } = null!;
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public int BrandId { get; set; }
        public Brands? Brand { get; set; }
        public List<ProductImages>? ProductImages { get; set; } = [];


        public ICollection<Comment> Comment { get; set; } = [];

        //public DateTime FirstDate { get; set; }
        //public DateTime LastDate { get; set; }
        //public int? DiscountPercentage { get; set; } = 0;
        //public DateTime? DiscountStartDate { get; set; }
        //public DateTime? DiscountEndDate { get; set; }
        //public bool InStock
        //{
        //    get
        //    {
        //        var now = DateTime.UtcNow;
        //        return Count > 0 && now >= FirstDate && now <= LastDate;
        //    }
        //}
        //public decimal DiscountedPrice
        //{
        //    get
        //    {
        //        if (DiscountPercentage.HasValue && DiscountPercentage.Value > 0)
        //        {
        //            return Price - (Price * DiscountPercentage.Value / 100);
        //        }
        //        return Price;
        //    }
        //}
        //public bool IsOnSale
        //{
        //    get
        //    {
        //        var now = DateTime.UtcNow;
        //        return DiscountPercentage.HasValue &&
        //               DiscountPercentage.Value > 0 &&
        //               (!DiscountStartDate.HasValue || DiscountStartDate <= now) &&
        //               (!DiscountEndDate.HasValue || DiscountEndDate >= now);
        //    }
        //}
    }
}
