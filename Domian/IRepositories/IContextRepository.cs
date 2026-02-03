using Domain.Models;

namespace Domain.IRepositories
{
    public interface IContextRepository : IRepository<Context>
    {
        Task<Context?> GetByQrCodeTokenAsync(string qrCodeToken);
        //Task<Context?> GetContextWithTabsAsync(int id);
        Task<Context?> GetContextWithActiveTabsAsync();
        Task<IEnumerable<Context>> GetContextsByContextPartAsync();
        Task<bool> IsQrCodeTokenUniqueAsync(string qrCodeToken);

    }
}
