using Final_Project.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(Db context) : base(context)
        {

        }

      

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await dbSet                
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Product> GetByIdWithAsync(int id)
        {
            return await dbSet
                 .Where(m => m.Id == id)
                 .Include(m => m.Category)
                 .Include(m => m.Brand)
                 .Include(m => m.ProductImages)
                 .Include(m => m.Comment)
                 //.AsNoTracking()
                 .FirstOrDefaultAsync();
        }

        public async Task<bool> ExistAsync(string name)
        {
            return await db.Products.AnyAsync(m => m.Name.Trim().ToLower() == name.Trim().ToLower());
          
        }

        public async Task<List<Product>> FilterAsync(string brandName, string categoryName, decimal? minPrice, decimal? maxPrice)
        {
            IEnumerable<Product> query = await GetAllAsync();
            List<Product> datas = new();

            if (brandName == null && categoryName == null && minPrice == null && maxPrice == null)
            {
                return query.ToList();
            }
            else if (brandName != null && categoryName != null && minPrice != null && maxPrice != null)
            {
                datas = query.Where(m =>
                    m.Brand.Name.ToLower() == brandName.ToLower() ||
                    m.Category.Name.ToLower() == categoryName.ToLower() ||
                    (m.Price >= minPrice && m.Price <= maxPrice)
                ).ToList();
            }
            else
            {
                if (brandName is not null)
                {
                    datas = query.Where(m => m.Brand.Name.ToLower() == brandName.ToLower()).ToList();
                }
                if (categoryName is not null)
                {
                    datas = query.Where(m => m.Category.Name.ToLower() == categoryName.ToLower()).ToList();
                }
                if (minPrice is not null && maxPrice is not null)
                {
                    datas = query.Where(m => m.Price >= minPrice && m.Price <= maxPrice).ToList();
                }
                else if (minPrice is not null)
                {
                    datas = query.Where(m => m.Price >= minPrice).ToList();
                }
                else if (maxPrice is not null)
                {
                    datas = query.Where(m => m.Price <= maxPrice).ToList();
                }
            }

            return datas;
        }

        public async Task<IEnumerable<Product>> SortByPriceDescending()
        {          
            return await dbSet.AsQueryable().OrderByDescending(m => m.Price).ToListAsync();
        }

        public async Task<IEnumerable<Product>> SortByPriceAscending()
        {
            return await dbSet.AsQueryable().OrderBy(m => m.Price).ToListAsync();

        }

        public async Task<IEnumerable<Product>> GetAllForSearchAsync()
        {
            return await dbSet
                   .Include(m => m.Category)
                 .Include(m => m.Brand)
                 .Include(m => m.ProductImages)
                 .Include(m => m.Comment)
                   .AsNoTracking()
                   .ToListAsync();
        }

        public  IQueryable<Product> GetAllWithIncludes(Expression<Func<Product, bool>> predicate)
        {
            return  dbSet
           .Include(m => m.Category)
                 .Include(m => m.Brand)
                 .Include(m => m.ProductImages)
                 .Include(m => m.Comment)    
                 .AsNoTracking()
            .Where(predicate);
        }

        public async Task<Product> GetByIdWithIncludes(int id)
        {
            return  await dbSet
           .Include(m => m.Category)
                 .Include(m => m.Brand)
                 .Include(m => m.ProductImages)
                 .Include(m => m.Comment)
          
            .FirstOrDefaultAsync();
        }

        public async Task DeleteProductImage(ProductImages image)
        {
            db.ProductImages.Remove(image);
            await db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetPaginateDatasAsync(int page, int take)
        {
            return await dbSet.Skip((page - 1) * take).Take(take).ToListAsync();
        }
    }
}
