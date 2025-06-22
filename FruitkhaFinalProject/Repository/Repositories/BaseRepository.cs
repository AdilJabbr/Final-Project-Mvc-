using Domain.Models.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly Db db;
        protected DbSet<T> dbSet;

        public BaseRepository(Db context)
        {
            db = context;
            dbSet = db.Set<T>();
        }

        public async Task CreateAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            dbSet.Remove(entity);
            await db.SaveChangesAsync();
        }

        public async Task EditAsync(T entity)
        {
            dbSet.Update(entity);
            await db.SaveChangesAsync();
        }
        public async Task<int> SaveChanges()
        {
            return await db.SaveChangesAsync();
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await dbSet.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            var query = dbSet.Where(predicate);
            return includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        public async Task<IEnumerable<T>> FindAll(Expression<Func<T, bool>> predicate)
        {
            return await dbSet.Where(predicate).ToListAsync();
        }

        public IQueryable<T> FindAllWithIncludes()
        {
            return dbSet.AsQueryable();
        }

        public async Task<bool> IsExist(Expression<Func<T, bool>> predicate = null)
        {
            return predicate == null ? false : await dbSet.AnyAsync(predicate);
        }

        public IQueryable<T> GetAllIncludes(params string[] includes)
        {

            IQueryable<T> query = dbSet;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return query;
        }
    }
}
