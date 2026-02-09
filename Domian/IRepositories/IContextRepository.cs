using Domain.Models;

namespace Domain.IRepositories
{
    public interface IContextRepository : IRepository<Context>
    {
        Task<Context?> CreateContextAsync(Context context);
        Task<Context?> GetContextWithActiveTabsAsync();
        Task<IEnumerable<Context>> GetContextsByContextPartAsync();
        Task<List<Context>> GetActiveAsync();

        //Task<bool> IsQrCodeTokenUniqueAsync(string qrCodeToken);
        //Task<bool> QRTokenExistsAsync(string qrToken);
        //Task<Context?> GetByQrCodeTokenAsync(string qrCodeToken);
    }
}
