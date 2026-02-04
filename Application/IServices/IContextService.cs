using Application.DTOs.Requests;
using Application.DTOs.Response;

namespace Application.IServices
{
    //Dont forget to use dtos and not models in services
    public interface IContextService
    {
        Task<CreateContextRequest?> CreateContextAsync(CreateContextRequest newContext);
        Task<ContextResponse?> GetContextByIdAsync(int id);
        Task<IEnumerable<ContextResponse>> GetAllContextsAsync();
        Task<ContextResponse?> GetContextPartByQrTokenAsync(string qrToken);
        Task<bool> RemoveContextAsync(int id);
        Task<bool> ActivateContextAsync(int contextId);
        string GenerateUniqueQRToken();
        //maybe this one more relant for context part
        Task<bool> IsContextActiveAsync(int contextId);
        Task<bool> ContextExistsAsync(int contextId);
    }
}
//Task<Context> UpdateContextAsync(int contextId, string name, string type)
//Task<bool> DeactivateContextAsync(int contextId)
//Task<string> RegenerateQRTokenAsync(int contextId)