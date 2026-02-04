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

        //tror helt ärligt inte att denna behövs
        public async Task<Context?> CreateContextAsync(Context context)
        {
            return await CreateAsync(context);
        }

        public async Task<List<Context>> GetActiveAsync()
        {
            //behöver man skriva equals true eller förstår den ändå
            var active =  _dbSet.Where(c => c.IsActive);

            return await active.ToListAsync();
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

        public async Task<bool> QRTokenExistsAsync(string qrToken)
        {
            return await _dbSet.AnyAsync(c => c.QrToken == qrToken);
        }
    }
}
