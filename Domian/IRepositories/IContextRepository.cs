using Domain.Models;

namespace Domain.IRepositories
{
    public interface IContextRepository : IRepository<Context>
    {
        Task<Context?> CreateContextAsync(Context context);
        Task<Context?> GetByQrCodeTokenAsync(string qrCodeToken);
        Task<Context?> GetContextWithActiveTabsAsync();
        Task<IEnumerable<Context>> GetContextsByContextPartAsync();
        Task<bool> IsQrCodeTokenUniqueAsync(string qrCodeToken);
        Task<bool> QRTokenExistsAsync(string qrToken);
        Task<List<Context>> GetActiveAsync();

    }
}
//Task<Context?> GetContextWithTabsAsync(int id);

//Task<Context> GetByIdAsync(int contextId)
//Task<Context> GetByQRTokenAsync(string qrToken)
//Task<List<Context>> GetAllAsync()
//Context Update(Context context)
//Task<bool> ExistsAsync(int contextId)