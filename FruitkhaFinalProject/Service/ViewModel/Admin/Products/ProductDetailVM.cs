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
    public class ProductDetailVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public string? Description { get; set; }
        public string Brand { get; set; }
        public IEnumerable<CommentVM> Comments { get; set; } = [];

        public IEnumerable<ProductImageVM> ProductImages { get; set; }
        public string Categories { get; set; }
    }
}
