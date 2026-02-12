//using Domain.IRepositories;
//using Domain.Models;
//using Infrastructure.Data;
//using Microsoft.EntityFrameworkCore;

//namespace Infrastructure.Repositories
//{
//    public class UserTabRepository : Repository<UserTab>, IUserTabRepository
//    {
//        public UserTabRepository(QrDbContext context) : base(context)
//        {
//        }

//        //active tabs
//        public async Task<IEnumerable<UserTab>> GetActiveAsync()
//        {
//            return await _dbSet
//                .Where(ut => ut.Status == TabStatus.Open)
//                .Include(u => u.ContextPart)
//                .ToListAsync();
//        }

//        public async Task<UserTab?> GetActiveByUserIdAsync(int userId)
//        {
//            return await _dbSet.Where(ut => ut.UserId == userId && ut.Status == TabStatus.Open).FirstOrDefaultAsync();
//        }

//        public async Task<UserTab?> GetActiveTabsByContextIdAsync(int contextId)
//        {
//            return await _dbSet.Where(ut => ut.ContextId == contextId && ut.Status == TabStatus.Open).FirstOrDefaultAsync();
//        }

//        //context tabs

//        public async Task<List<UserTab>> GetAllByContextIdAsync(int contextId)
//        {
//            return await _dbSet.Where(ut => ut.ContextId == contextId).ToListAsync();
//        }

//        public async Task<List<UserTab>> GetByContextIdAsync(int contextId)
//        {
//            return await _dbSet.Where(ut => ut.ContextId == contextId).ToListAsync();
//        }

//        public async Task<List<UserTab>> GetByUserIdAsync(int userId)
//        {
//            //var user =  await _dbSet.Include(u => u.User).Select(u => u.User).
//            //    FirstOrDefaultAsync(u => u.Id == userId);
//            //return user;

//            throw new NotImplementedException();
//        }

//        public async Task<IEnumerable<UserTab>> GetClosedAsync()
//        {
//            return await _dbSet.Where(ut => ut.Status == TabStatus.Closed).ToListAsync();
//        }

//        public Task<IEnumerable<UserTab>> GetOpenTabsByContextIdAsync(int contextId)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<IEnumerable<UserTab>> GetStatus()
//        {
//            throw new NotImplementedException();
//        }

//        //USer
//        public async Task<IEnumerable<UserTab>> GetTabsByUserIdAsync(int userId)
//        {
//            //var user = await _dbSet.Include(u => u.User)
//            //    .Where(u => u.UserId == userId)
//            //    .Include(u => u.ContextPart)
//            //    .Select(u => u.ContextPartId)
//            //    .ToListAsync();


//            return await _dbSet.Where(ut => ut.UserId == userId).ToListAsync();
//        }
//        public async Task<User> GetParticipantCountAsync(int userId)
//        {
//            throw new NotImplementedException();
//        }

//        public async Task<decimal> GetTotalAsync(int tabId)
//        {
//            return await _dbSet.Where(ut => ut.Id == tabId)
//                .SelectMany(ut => ut.Transactions)
//                .SumAsync(t => t.Product.Price);
//        }

//        //details for a tab thorugh transactions and payments from the id that is from the user tab
//        public async Task<UserTab?> GetTabWithDetailsAsync(int id)
//        {
//            var tab = await _dbSet.Select(ut => ut)
//                .Include(ut => ut.Transactions)
//                .ThenInclude(t => t.Select(t => t.User.FirstName))
//                .ToListAsync();
//            //.Include(ut => ut.User)

//            return tab.FirstOrDefault(ut => ut.Id == id);
//        }

//        public async Task<bool> IsTabClosedAsync(int tabId)
//        {
//            return await _dbSet.AnyAsync(ut => ut.Id == tabId && ut.Status == TabStatus.Closed);
//        }

//        public async Task<bool> IsTabOpenAsync(int tabId)
//        {
//            return await _dbSet.AnyAsync(ut => ut.Id == tabId && ut.Status == TabStatus.Open);
//        }

//        public async Task<bool> IsTabPaidAsync(int tabId)
//        {
//            return await _dbSet.AnyAsync(ut => ut.Id == tabId && ut.Status == TabStatus.Paid);
//        }
//    }
//}