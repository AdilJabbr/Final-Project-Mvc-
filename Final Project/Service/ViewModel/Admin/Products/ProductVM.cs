using Service.DTOs.Admin.Categories;
using Service.ViewModel.Admin.Brands;
using Service.ViewModel.Admin.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.Admin.Products
{
    public class ProductVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public string? Description { get; set; }
        public IEnumerable<CommentVM> Comments { get; set; } = [];
        public string? IsMainPicture { get; set; }
        public string? CategoryName { get; set; }
        public int CategoryId { get; set; }
        public List<CategoryVM> Categories { get; set; } = new();
        public string? BrandName { get; set; }
        public int BrandId { get; set; }
        public List<BrandVM> Brands { get; set; } = new();

        public List<ProductImageVM>? ProductImages { get; set; }
    }
}
