using System.Linq.Expressions;

namespace Domain.IRepositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> CreateAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(object id);

        //looking for an object that matches a condition - predicate = condition
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteBoolAsync(object id);
        Task DeleteAsync(object id);
        Task<bool> ExistsAsync(object id);
    }
}
