using Final_Project.Models;
using Microsoft.AspNetCore.Http;
using Service.DTOs.Admin.Products;
using Service.Helpers;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace service.services.ınterfaces
{
    public interface IProductService 
    {

        Task<ProductCreateVM> GetCreatedProductDto();
        Task<ProductEditVM> GetUpdateProduct(int id);
        Task<(bool Success, List<string> Errors)> ProductCreate(ProductCreateVM dto);
        Task<bool> DeleteAsync(int id);
        Task<ProductVM> GetProduct(int id);
        Task<ProductEditVM> GetProductUpdateDto(int productId);
        Task<(bool Success, List<string> Errors)> UpdateProductAsync(ProductEditVM dto);



        // Task CreateAsync(ProductCreateVM model);
        Task<IEnumerable<ProductVM>> GetAllAsync();
        Task<ProductVM> GetByIdAsync(int? id);
        Task<ProductDetailVM> DetailAsync(int? id);
       // Task DeleteAsync(int? id);

       // Task EditAsync(int? id, ProductEditVM model);


        Task<PaginationResponse<ProductVM>> GetPaginateAsync(int page, int take);
        Task<IEnumerable<ProductVM>> SearchByName(string name);

        Task<IEnumerable<ProductVM>> FilterAsync(string brandName, string categoryName, decimal? minPrice, decimal? maxPrice);
        Task<IEnumerable<ProductVM>> SortByPriceAscending();

        Task<IEnumerable<ProductVM>> SortByPriceDescending();

        //Task AddImageAsync(int productId, IFormFile uploadImage);
        //Task DeleteImageAsync(int productId, int productImageId);

    }
}
