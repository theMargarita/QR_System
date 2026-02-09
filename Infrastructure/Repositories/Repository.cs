using Domain.IRepositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly QrDbContext _context;
        protected readonly DbSet<T> _dbSet;
        public Repository(QrDbContext context) 
        { 
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public virtual async Task<T> CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        //must every int be an obejct now
        public virtual async Task DeleteAsync(int id)
        {
            var foundId = await _dbSet.FindAsync(id); 
            if (foundId != null)
            {
                _dbSet.Remove(foundId);
            }
        }

        public virtual async Task<bool> DeleteBoolAsync(int id)
        {
            var findId = await _dbSet.FindAsync(id);
            if(findId != null)
            {
                _dbSet.Remove(findId);
                return true;
            }
            return false;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _dbSet.FindAsync(id) != null;
        }

        public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T?> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            return entity;
        }
    }
    public static class StringHelper
    {
        public static bool EqualsIgnoreCase(string? a, string? b)
            => string.Equals(a, b, StringComparison.OrdinalIgnoreCase);

        public static bool ContainsIgnoreCase(string source, string value)
            => source.Contains(value, StringComparison.OrdinalIgnoreCase);

        public static string Normalize(string value)
            => value.Trim().ToUpperInvariant();
    }
}
