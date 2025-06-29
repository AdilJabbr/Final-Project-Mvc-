using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModel.Admin.Products
{
    public class ProductImageCreateVM
    {
        public IFormFile ImageUrl { get; set; } = null!;

    }
}
