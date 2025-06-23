using Microsoft.AspNetCore.Http;
using Service.DTOs.Admin.Products;
using Service.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace service.services.ınterfaces
{
    public interface IProductService
    {
        Task CreateAsync(ProductCreateVM model);
        Task<IEnumerable<ProductVM>> GetAllAsync();
        Task<ProductVM> GetByIdAsync(int? id);
        Task<ProductDetailVM> DetailAsync(int? id);
        Task DeleteAsync(int? id);

        Task EditAsync(int? id, ProductEditVM model);


        Task<PaginationResponse<ProductVM>> GetPaginateAsync(int page, int take);
        Task<IEnumerable<ProductVM>> SearchByName(string name);

        Task<IEnumerable<ProductVM>> FilterAsync(string brandName, string categoryName, decimal? minPrice, decimal? maxPrice);
        Task<IEnumerable<ProductVM>> SortByPriceAscending();

        Task<IEnumerable<ProductVM>> SortByPriceDescending();

        Task AddImageAsync(int productıd, IFormFile uploadımage);
        Task DeleteImageAsync(int productıd, int productımageıd);

    }
}
