using Final_Project.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.Admin.Products
{
    public class ProductEditVM
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public string? Description { get; set; }
        public int BrandId { get; set; }
        public Brands Brands { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<ProductImages> ProductImages { get; set; }
        public List<IFormFile> UploadImages { get; set; }
    }
}
