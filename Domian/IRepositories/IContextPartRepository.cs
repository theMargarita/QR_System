using Domain.Models;
using Domian.Models;

namespace Domain.IRepositories
{
    public interface IContextPartRepository : IRepository<ContextPart>
    {
        // QR Code methods
        Task<ContextPart?> GetByQrTokenAsync(string qrToken);
        Task<bool> QRTokenExistsAsync(string qrToken);

        // Query methods
        Task<List<ContextPart>> GetPartsByContextIdAsync(int contextId);
        Task<ContextPart?> GetByIdWithDetailsAsync(int contextPartId);
        Task<List<ContextPart>> GetActivePartsAsync();
        Task<List<ContextPart>> GetOccupiedPartsAsync(int contextId);

        // User tab methods
        Task<bool> UserHasActiveTabAsync(int contextPartId, int userId);
        Task<UserTab?> GetUserActiveTabAsync(int contextPartId, int userId);
        Task<int> GetActiveUserCountAsync(int contextPartId);
    }
}


