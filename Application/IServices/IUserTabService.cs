using Application.DTOs.UserTabFolder.AdminTabFolder;
using Application.DTOs.UserTabFolder.Request;
using Application.DTOs.UserTabFolder.Resonse;
using Application.DTOs.UserTabFolder.Response;
using Domain.Models;


namespace Application.IServices
{
    public interface IUserTabService
    {
        Task<IEnumerable<ActiveTabSummary>> GetActiveTabAsync();
        Task<UserTabResponse?> GetTabByIdAsync(Guid id);
        Task<IEnumerable<UserTabResponse>> GetAllAsync();
        Task<UserTabResponse> CreateAsync(Guid contextPartId, Guid userId);
        Task<UserTabResponse> UpdateAsync(Guid id);
        Task<bool> DeleteAsync(Guid id);
        Task<UserDetailTabResponses?> GetTabDetailsAsync(Guid tabId);
        Task<OpenTabRequest> ToOrder(string qrToken);

        //to controll if user has opened a tab
        Task<bool> HasOpenTabAsync(Guid userId);
        //admin close the tab
        Task<bool> CloseTabAsync(Guid tabId);
        //check if the tab is paid
        Task<bool> IsTabPaidAsync(Guid tabId);
    }
}
