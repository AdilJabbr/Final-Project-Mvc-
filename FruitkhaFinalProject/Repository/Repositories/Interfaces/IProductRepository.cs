using Final_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdWithAsync(int id);
        Task<bool> ExistAsync(string name);

        Task<IEnumerable<Product>> GetPaginateDatasAsync(int page, int take);

        Task<List<Product>> FilterAsync(string brandName, string categoryName, decimal? minPrice, decimal? maxPrice);
        Task<IEnumerable<Product>> SortByPriceDescending();
        Task<IEnumerable<Product>> SortByPriceAscending();

        Task<IEnumerable<Product>> GetAllForSearchAsync();
        IQueryable<Product> GetAllWithIncludes(Expression<Func<Product, bool>> predicate);
        Task<Product> GetByIdWithIncludes(int id);
        Task DeleteProductImage(ProductImages image);
        //Task<IEnumerable<Product>> GetAllWithCategoryAndBrandAsync();
        //Task<IEnumerable<Product>> GetAllWithCategoryAsync();
        //Task<IEnumerable<Product>> GetAllWithBrandAsync();
        //Task<Product> GetByIdWithCategoryAsync(int id);
        //Task<Product> GetByIdWithBrandAsync(int id);
        //Task<IEnumerable<Product>> FilterByCategoryAsync(string name, string categoryName);
        //Task<IEnumerable<Product>> FilterByBrandAsync(string name, string brandName);
        //Task<Product> GetByIdWithBrandAndCategoryAsync(int id);
        //Task<IEnumerable<Product>> GetPaginateDatasAsync(int page, int take);
    }
}
