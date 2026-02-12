using Application.DTOs.ContextPartFolder.Request;
using Application.DTOs.CPFolder.Response;

namespace Application.IServices
{
    public interface IContextPartService
    {
        Task<bool> ActivateTableAsync(Guid contextPartId, Guid userId);
        Task<ContextPartResponse?> CreateContextPartAsync(CreateContexPartRequest request);
        Task<int> GetActiveUserCountAsync(Guid contextPartId);
        Task<ContextPartResponse?> GetOrCreateActiveTabAsync(Guid cpId, Guid userId);

        Task<ContextPartResponse?> ScanQrTokenAsync(string qrToken);

        Task<int?> GetUserActiveTabAsync(Guid contextPartId);

        //tycker inte riktigt att denna är relevant
        Task<bool> CheckTable(int contextPartId);

        Task<bool> RemoveContextPartAsync(Guid contextPartId);
        Task<ContextPartResponse> GetContextPartByIdAsync(Guid contextPartId);
    }
}
