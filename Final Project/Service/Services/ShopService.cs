using service.services.ınterfaces;
using Service.DTOs.Admin.Products;
using Service.ViewModel.Admin.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ShopService
    {
        private readonly IProductService _productService;

        public ShopService(IProductService productService)
        {
            _productService = productService;
        }

       
        public async Task<ShopVM> GetShop()
        {
            var products =  await _productService.GetAllAsync();

            var shopDto = new ShopVM
            {
                Products = products.ToList()
            };

            return shopDto;
        }

        public async Task<ShopVM> GetShop(string? search, string? sort, int? categoryId = null)
        {
            var products = await _productService.GetAllAsync();

            if (!string.IsNullOrWhiteSpace(search))
            {
                products = products
                    .Where(p => p.Name.Contains(search, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            if (categoryId.HasValue)
            {
                products = products
                    .Where(p => p.CategoryId == categoryId.Value)
                    .ToList();
            }

            products = sort switch
            {
                "az" => products.OrderBy(p => p.Name).ToList(),
                "za" => products.OrderByDescending(p => p.Name).ToList(),
                "priceLowHigh" => products.OrderBy(p => p.Price).ToList(),
                "priceHighLow" => products.OrderByDescending(p => p.Price).ToList(),
                _ => products
            };

            return new ShopVM
            {
                Products = products.ToList()
            };
        }
    }
}
