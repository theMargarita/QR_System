using Domain.IRepositories;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ContextRepository : Repository<Context>, IContextRepository
    {
        public ContextRepository(QrDbContext context) : base(context)
        {
        }

        // Get Context by QrCodeToken - not sure about this one
        public async Task<Context?> GetByQrCodeTokenAsync(string qrCodeToken)
        {
            return _dbSet.FirstOrDefault(c => c.QrToken == qrCodeToken);
          
        }

        //hämta alla contexts som har en contextpart med angivet namn - context = ölkyl etc, contextparts = bord etc
        public async Task<IEnumerable<Context>> GetContextsByContextPartAsync()
        {
            return _dbSet.Include(c => c.Parts)
                           .Where(c => c.Parts != null && c.Parts.Any())
                           .ToList();
        }

        public async Task<Context?> GetContextWithActiveTabsAsync()
        {
            return _dbSet
                .Include(c => c.Parts)
                .Where(c => c.IsActive)
                .FirstOrDefault();
        }

        //not sure about htis one
        public async Task<bool> IsQrCodeTokenUniqueAsync(string qrCodeToken)
        {
            return !await _dbSet.AnyAsync(c => c.QrToken == qrCodeToken);
        }
    }
}
