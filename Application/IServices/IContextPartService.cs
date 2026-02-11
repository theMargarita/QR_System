using Application.DTOs.ContextPartFolder.Request;
using Application.DTOs.CPFolder.Response;

namespace Application.IServices
{
    public interface IContextPartService
    {
        Task<bool> ActivateTableAsync(Guid contextPartId, Guid userId);
        Task<ContextPartResponse?> CreateContextPartAsync(CreateContexPartRequest request);
        Task<int> GetActiveUserCountAsync(Guid contextPartId);
        Task<ContextPartResponse?> GetOrCreateActiveTabAsync(/*Guid cpId, Guid userId*/);
        //  scanning QR code => go to contextpart dashboard => choose products => add to order == transaction added to usertab 
        Task<ContextPartResponse?> ScanQrTokenAsync(string qrToken);

       Task<ContextPartResponse?> GetUserActiveTabAsync(Guid contextPartId, Guid userId);

        //tycker inte riktigt att denna är relevant
        Task<bool> CheckTable(int contextPartId);

       Task<bool> RemoveContextPartAsync(Guid contextPartId);
    }
}
