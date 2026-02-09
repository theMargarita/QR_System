using Domain.IRepositories;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(QrDbContext context) : base(context)
        {
        }

        public async Task<User>? CreateUserAsync(User user)
        {
            await _dbSet.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task RemoveUserAsync(int id)
        {
            var user = await _dbSet.FindAsync(id);
            if (user != null)
            {
                _dbSet.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<User?> GetUserByPhoneAsync(string phoneNumber)
        {
            return await _dbSet.Where(u => u.PhoneNumber == phoneNumber).FirstOrDefaultAsync();
        }

        public async Task<User?> GetUserByNameAsync(string name)
        {
            return await _dbSet.FirstOrDefaultAsync(u => 
            u.FirstName == name.ToLower() ||
            u.LastName == name.ToLower());
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var users = _dbSet.ToList();
            return users;
        }

        //kanske kan vara bra att får användaren genom namn eller id? smidigare med id kanske
        //kasnke äe r onödigt
        public async Task<IEnumerable<User>> GetUsersWithTransactionsAsync()
        {
            return await _dbSet.Include(u => u.Transactions)
                .ThenInclude(t => t.Product)
                .ToListAsync();
        }

        public async Task<IEnumerable<User>> GetUsersWithTabsAsync()
        {
           return await _dbSet.Include(u => u.Tabs)
                .ThenInclude(t => t.Transactions).ToListAsync();
        }
        public async Task<User?> GetUserProfileAsync(int userId)
        {
            return await _dbSet
                .Include(u => u.Tabs!
                .Where(t => t.Status == TabStatus.Open))
                    .ThenInclude(t => t.Transactions)
                    .ThenInclude(t => t.Tab.Payments)
                .Include(u => u.Transactions!
                .OrderByDescending(t => t.CreatedAt).Take(10))
                    .ThenInclude(t => t.Product)
                .Include(u => u.Payments!
                .OrderByDescending(p => p.CreatedAt).Take(10))
                .AsSplitQuery() 
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<IEnumerable<User>> SearchUsersAsync(string searchTerm)
        {
            return await _dbSet
                .Where(u =>
                    u.FirstName.Contains(searchTerm) ||
                    u.LastName.Contains(searchTerm) ||
                    (u.PhoneNumber != null && u.PhoneNumber.Contains(searchTerm)))
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .Take(20)
                .ToListAsync();
        }
    }
}
