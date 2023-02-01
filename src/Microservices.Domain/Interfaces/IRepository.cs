using System.Linq.Expressions;

namespace Microservices.Domain.Interfaces
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        IQueryable<T> GetAllAsQueryAsync();
        Task<T> GetAsync(object id);
        Task<T> AddAsync(T entity);
        void Update(T entity);
        Task DeleteAsync(object id);
        int Count();

        void DetachLocal(Func<T, bool> predicate);

        Task WithTransactionAsync(Action transacton);
    }
}