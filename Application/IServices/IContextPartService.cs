using Application.DTOs.ContextPartFolder.Request;
using Application.DTOs.CPFolder.Response;
using Domain.Models;

namespace Application.IServices
{
    public interface IContextPartService 
    {
        string GenerateUniqueQRToken();
        Task<ContextPartResponse?> GetContextPartByQrTokenAsync(string qrToken);
        Task<CreateContexPartRequest?> CreateContextPartAsync(CreateContexPartRequest request);
        Task<bool> RemoveContextPartAsync(int contextPartId);
        Task<bool> IsContextPartOccupiedAsync(int contextPartId);

        // User tab methods
        Task<bool> UserHasActiveTabAsync(int contextPartId, int userId);
        Task<ContextPartResponse?> GetUserActiveTabAsync(int contextPartId, int userId);
        Task<int> GetActiveUserCountAsync(int contextPartId);
    }
}

//// QR Code methods
//Task<ContextPart?> GetByQrTokenAsync(string qrToken);
//Task<bool> QRTokenExistsAsync(string qrToken);

//// Query methods
//Task<List<ContextPart>> GetPartsByContextIdAsync(int contextId);
//Task<ContextPart?> GetByIdWithDetailsAsync(int contextPartId);
//Task<List<ContextPart>> GetActivePartsAsync();
//Task<List<ContextPart>> GetOccupiedPartsAsync(int contextId);
