using Domian.Models;

namespace Domain.IRepositories
{
    public interface IContextPartRepository
    {
        //is this really nessary when there is unit of work?
        Task<ContextPart> CreateContextPartAsync(ContextPart contextPart);
        Task<ContextPart> GetByIdAsync(int contextPartId);
        //active tables 
        Task<ContextPart> GetActivePartAsync(int userId, int tabId);
        Task<bool> QRTokenExistsAsync(string qrToken);

        //Do i need this? maybe not
        //Task<List<ContextPart>> GetByTabIdAsync(int tabId);
        ContextPart Update(ContextPart contextPart);
        Task<bool> ExistsAsync(int userId, int tabId);
        //might need to change this to count how many user is active on a specific table
        Task<int> AmoutOfUserOnAPartAsync();
    }
}
