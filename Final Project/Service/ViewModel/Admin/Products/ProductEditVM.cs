using Final_Project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.Admin.Products
{
    public class ProductEditVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public string? Description { get; set; }
        public string? ImageMain { get; set; }
        public IFormFile? MainImageUrl { get; set; }

        public List<ProductImageVM> imgUrl { get; set; } = [];
        public List<IFormFile> ProductImages { get; set; } = [];
        public int BrandId { get; set; }

        public List<SelectListItem> Brands { get; set; } = new List<SelectListItem>();

        public int CategoryId { get; set; }

        public List<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
    }
}
