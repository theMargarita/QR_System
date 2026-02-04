using Application.DTOs.Requests;
using Application.DTOs.Response;

namespace Application.IServices
{
    //Dont forget to use dtos and not models in services
    public interface IContextService
    {
        Task<IEnumerable<ContextResponse>> GetAllContextsAsync();
        Task<ContextResponse?> GetContextByIdAsync(int id);
        Task<ContextResponse?> GetContextPartByQrTokenAsync(string qrToken);
        Task<CreateContextRequest?> CreateContextAsync(CreateContextRequest newContext);
        Task<bool> RemoveContextAsync(int id);
    }
}
