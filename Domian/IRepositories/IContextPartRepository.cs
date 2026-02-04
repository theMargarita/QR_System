using Domian.Models;

namespace Domain.IRepositories
{
    public interface IContextPartRepository
    {
        //is this really nessary when there is unit of work?
        Task<ContextPart> CreateAsync(ContextPart contextPart);
        Task<ContextPart> GetByIdAsync(int contextPartId);
        Task<ContextPart> GetActiveAsync(int userId, int tabId);
        Task<List<ContextPart>> GetByTabIdAsync(int tabId);
        Task<List<ContextPart>> GetActiveByTabIdAsync(int tabId);
        Task<List<ContextPart>> GetByUserIdAsync(int userId);
        ContextPart Update(ContextPart contextPart);
        void Delete(ContextPart contextPart);
        Task<bool> ExistsAsync(int userId, int tabId);
        Task<int> CountActiveByTabIdAsync(int tabId);
    }
}
