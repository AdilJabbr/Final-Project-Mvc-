using Service.DTOs.Admin.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModel.Admin.Products
{
    public class ProductFilterVM
    {
        public List<ProductVM> Products { get; set; }

        public List<int>? SelectedCategoryIds { get; set; }
        public List<int>? SelectedBrandIds { get; set; }

        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }

        public string? Search { get; set; }
        public string? Sort { get; set; }
    }
}
