using Service.DTOs.Admin.Categories;
using Service.DTOs.Admin.Products;
using Service.ViewModel.Admin.Brands;

namespace Service.ViewModel.Admin.Shop
{
    public class ShopVM
    {
        public decimal TotalCount { get; set; }
        public List<ProductVM> Products { get; set; } = [];
        public List<CategoryVM> Categories { get; set; } = [];
        public List<BrandVM> Brands { get; set; } = [];
    }
}
