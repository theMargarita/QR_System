using Application.DTOs.ContextFolder.Request;
using Application.DTOs.ContextFolder.Response;

namespace Application.IServices
{
    //Dont forget to use dtos and not models in services
    public interface IContextService
    {
        Task<ContextResponse?> CreateContextAsync(CreateContextRequest request);
        Task<ContextResponse?> GetContextByIdAsync(Guid id);
        Task<IEnumerable<ContextResponse>> GetAllContextsAsync();
        
        Task<bool> RemoveContextAsync(Guid id);
        //Task<bool> ActivateContextAsync(int contextId);

        //maybe this one more relant for context part
        Task<IEnumerable<ContextResponse>> SearchContext(string searchTerm);
        //Task<bool> ContextExistsAsync(int contextId);
    }
}
//Task<Context> UpdateContextAsync(int contextId, string name, string type)
//Task<bool> DeactivateContextAsync(int contextId)
//Task<string> RegenerateQRTokenAsync(int contextId)