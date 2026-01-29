using Domain.Models;

namespace Domain.IRepositories
{
    public interface IContextRepository : IRepository<Context>
    {
        Task<Context?> GetByQrCodeTokenAsync(string qrCodeToken);
        Task<Context?> GetContextWithTabsAsync(int id);
        Task<Context?> GetContextWithActiveTabsAsync(int id);
        Task<IEnumerable<Context>> GetContextsByTypeAsync(string type);
        Task<bool> IsQrCodeTokenUniqueAsync(string qrCodeToken);

    }
}
