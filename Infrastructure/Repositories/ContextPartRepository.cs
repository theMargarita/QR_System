using Domain.IRepositories;
using Domain.Models;
using Domian.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ContextPartRepository : Repository<ContextPart>, IContextPartRepository
    {
        public ContextPartRepository(QrDbContext context) : base(context)
        {
        }

        public async Task<List<ContextPart>> GetPartsByContextIdAsync(int contextId)
        {
            return await _dbSet
                .Where(ContextId => ContextId.Id == contextId)
                .Include(cp => cp.UserTabs)
                .OrderBy(cp => cp.Name)
                .ToListAsync();
        }

        //get contextpart with details by id
        public async Task<ContextPart?> GetByIdWithDetailsAsync(int contextPartId)
        {
            return await _dbSet
                .Include(cp => cp.Context)
                .Include(cp => cp.UserTabs)
                .FirstOrDefaultAsync(cp => cp.Id == contextPartId);
        }

        public async Task<List<ContextPart>> GetActivePartsAsync()
        {
            return await _dbSet
                .Where(cp => cp.IsActive)
                .Include(cp => cp.Context)
                .ToListAsync();
        }

        public async Task<List<ContextPart>> GetOccupiedPartsAsync(int contextId)
        {
            return await _dbSet
                .Where(cp => cp.Context.Id == contextId && cp.UserTabs
                .Any(ut => !ut.IsClosed))
                .Include(cp => cp.UserTabs
                .Where(ut => !ut.IsClosed))
                .ToListAsync();
        }

        public async Task<bool> UserHasActiveTabAsync(int contextPartId, int userId)
        {
           return await _dbSet
             .Where(cp => cp.Id == contextPartId)
             .SelectMany(cp => cp.UserTabs)
             .AnyAsync(ut => ut.UserId == userId && !ut.IsClosed);
        }

        public async Task<UserTab?> GetUserActiveTabAsync(int contextPartId, int userId)
        {
            return await _dbSet
             .Where(cp => cp.Id == contextPartId)
             .SelectMany(cp => cp.UserTabs)
             .FirstOrDefaultAsync(ut => ut.UserId == userId);
        }

        public async Task<int> GetActiveUserCountAsync(int contextPartId)
        {
            return await _dbSet
               .Where(cp => cp.Id == contextPartId)
               .SelectMany(cp => cp.UserTabs)
               .Where(ut => !ut.IsClosed)
               .Select(ut => ut.UserId)
               .Distinct()
               .CountAsync();
        }

        //QR PART
        //will see if I use this one
        public async Task<ContextPart?> GetByQrTokenAsync(string qrToken)
        {
            return await _dbSet
                .Include(cp => cp.Context)
                .Include(cp => cp.UserTabs)
                .Where(cp => cp.IsActive)
                .FirstOrDefaultAsync(c => c.QrToken == qrToken);
        }
        public async Task<bool> IsQrCodeTokenUniqueAsync(string qrToken)
        {
            return !await _dbSet.AnyAsync(c => c.QrToken == qrToken);
        }
        public async Task<bool> QRTokenExistsAsync(string qrToken)
        {
            return await _dbSet.AnyAsync(c => c.QrToken == qrToken);
        }
    }
}
