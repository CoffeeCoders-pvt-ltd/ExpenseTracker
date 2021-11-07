using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ExpenseTracker.Common.Pagination;
using ExpenseTracker.Common.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Infrastructure.Repositories.Implementation
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private DbContext _context;
        private DbSet<T> _currentSession;

        public GenericRepository(DbContext context)
        {
            _context = context;
            _currentSession = _context.Set<T>();
        }

        public async Task<T> FindAsync(long id) => await _currentSession.FindAsync(id);


        public async Task<T> FindOrThrowAsync(long id)
            => await FindAsync(id) ?? throw new Exception("Request item not found");


        public void Delete(T entities)
        {
            _currentSession.Remove(entities);
        }

        public void Create(T entities)
        {
            _currentSession.Add(entities);
        }

        public async Task CreateAsync(T entities)
        {
            await _currentSession.AddAsync(entities).ConfigureAwait(false);
        }

        public void Update(T entities)
        {
            _currentSession.Update(entities);
        }

        public Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null)
        {
            predicate ??= x => true;
            return _context.Set<T>().Where(predicate).ToListAsync();
        }


        public IQueryable<T> GetQueryable()
        {
            return _currentSession.AsQueryable();
        }

        public T Find(long id)
        {
            return _currentSession.Find(id);
        }

        public virtual async Task<T> GetByIdAsync(long id)
        {
            return await _currentSession.FindAsync(id);
        }

        public IQueryable<T> GetPredicatedQueryable(Expression<Func<T, bool>>? predicate)
        {
            return predicate == null ? GetQueryable() : GetQueryable().Where(predicate);
        }

        public async Task<bool> CheckIfExistAsync(Expression<Func<T, bool>> predicate)
        {
            return await GetPredicatedQueryable(predicate)
                .CountAsync()
                .ConfigureAwait(false) != 0;
        }

        public Pagination<T> Paginate(IQueryable<T> queryable, int page = 1, int limit = 100)
        {
            return new Pagination<T>(
                queryable.Skip((page - 1) * limit).Take(limit).ToList(),
                queryable.Count(),
                page,
                limit
            );
        }
    }
}