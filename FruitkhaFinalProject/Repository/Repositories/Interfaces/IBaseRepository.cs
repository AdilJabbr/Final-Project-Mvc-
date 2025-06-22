using Domain.Models.Common;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task CreateAsync(T entity);
        Task EditAsync(T entity);
        IQueryable<T> FindAllWithIncludes();
        Task DeleteAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        IQueryable<T> GetAllIncludes(params string[] includes);

        Task<T> GetById(int id);
        Task<int> SaveChanges();

        Task<IEnumerable<T>> FindAll(Expression<Func<T, bool>> predicate);
        public Task<bool> IsExist(Expression<Func<T, bool>> predicate = null);

        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);

        //IQueryable<T> GetAll(Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, bool ignoreQueryFilter = false, bool asNotTracking = true);

        //Task<T?> GetByIdAsync(int id, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, bool ignoreQueryFilter = false, bool asNotTracking = true);
        //IQueryable<T> GetFilter(Expression<Func<T, bool>> expression, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, bool ignoreQueryFilter = false, bool asNotTracking = true);

        //Task<IEnumerable<T>> FindAll(Expression<Func<T, bool>> predicate);
        //IQueryable<T> FindBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);

        //IQueryable<T> Paginate(IQueryable<T> query, int limit, int page);
        //Task<bool> IsExistAsync(Expression<Func<T, bool>> expression);
    }
}
