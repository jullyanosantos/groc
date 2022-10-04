using Microservices.Domain.Interfaces;
using Microservices.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Microservices.Infrastructure.Data.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        private AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        #region Public Methods
               
        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);

            return entity;
        }

        public int Count()
        {
            return _dbSet.Count();
        }

        public async Task DeleteAsync(object id)
        {
            var entity = await GetAsync(id);

            if (entity != null)
            {
                if (_context.Entry(entity).State == EntityState.Detached)
                {
                    _dbSet.Attach(entity);
                }

                _dbSet.Remove(entity);
            }
        }

        public async Task<T?> GetAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public IQueryable<T> GetAllAsQueryAsync()
        {
            return _dbSet.AsQueryable<T>();
        }

        #endregion
    }
}
