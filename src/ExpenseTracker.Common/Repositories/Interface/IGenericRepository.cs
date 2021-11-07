using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ExpenseTracker.Common.Pagination;

namespace ExpenseTracker.Common.Repositories.Interface
{
    public interface IGenericRepository<T>
    {
        Task<T> FindAsync(long id);
        Task<T> FindOrThrowAsync(long id);
        void Delete(T entities);
        void Create(T entities);
        Task CreateAsync(T entities);
        void Update(T entities);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null);
        IQueryable<T> GetPredicatedQueryable(Expression<Func<T, bool>>? predicate);
        IQueryable<T> GetQueryable();
        T Find(long id);
        Task<T?> GetByIdAsync(long id);

        Task<bool> CheckIfExistAsync(Expression<Func<T, bool>> predicate);

        Pagination<T> Paginate(IQueryable<T> queryable, int page = 1, int limit = 100);
    }
}