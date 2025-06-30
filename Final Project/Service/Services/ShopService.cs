using service.services.ınterfaces;
using Service.Services.Interfaces;
using Service.ViewModel.Admin.Shop;

namespace Service.Services
{
    public class ShopService : IShopService
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;

        private int _pageSize = 4;


        public ShopService(IProductService productService,
                            ICategoryService categoryService,
                            IBrandService brandService)
        {
            _productService = productService;
            _categoryService = categoryService;

            _brandService = brandService;

        }


        public async Task<ShopVM> GetShop()
        {
            var products = await _productService.GetAllAsync();
            var categories = await _categoryService.GetAllAsync();
            var brands = await _brandService.GetAllAsync();

            var shopVM = new ShopVM
            {
                Products = products.ToList(),
                Categories = categories.ToList(),
                Brands = brands.ToList()
            };

            return shopVM;
        }

        public async Task<ShopVM> GetShop(int pageRow, string? search, int? sort, List<int>? categoryIds, List<int>? brandIds, int? maxPrice = null, int? minPrice = null)
        {
            var products = await _productService.GetAllAsync();
            var categories = await _categoryService.GetAllAsync();
            var brands = await _brandService.GetAllAsync();

            if (!string.IsNullOrWhiteSpace(search))
            {
                products = products
                    .Where(p => p.Name.Contains(search, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            if (categoryIds.Count is not 0)
            {
                products = products
                    .Where(p => categoryIds.Contains(p.CategoryId))
                    .ToList();
            }
            if (brandIds.Count is not 0)
            {
                products = products
                    .Where(p => brandIds.Contains(p.BrandId))
                    .ToList();
            }
            if (maxPrice.HasValue)
            {
                products = products
                    .Where(p => p.Price <= maxPrice)
                    .ToList();
            }
            if (minPrice.HasValue)
            {
                products = products
                    .Where(p => p.Price >= minPrice)
                    .ToList();
            }

            products = sort switch
            {
                1 => products.OrderBy(p => p.Name).ToList(),
                2 => products.OrderByDescending(p => p.Name).ToList(),
                3 => products.OrderBy(p => p.Price).ToList(),
                4 => products.OrderByDescending(p => p.Price).ToList(),
                _ => products
            };



            return new ShopVM
            {
                TotalCount = Math.Ceiling((decimal)products.Count() / _pageSize),
                Products = products.Skip((pageRow - 1) * _pageSize).Take(_pageSize).ToList(),
                Categories = categories.ToList(),
                Brands = brands.ToList()
            };
        }
    }
}
