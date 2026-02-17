using Application.DTOs.UserTabFolder.AdminTabFolder;
using Application.DTOs.UserTabFolder.Request;
using Application.DTOs.UserTabFolder.Resonse;
using Application.DTOs.UserTabFolder.Response;


namespace Application.IServices
{
    public interface IUserTabService
    {
        Task<IEnumerable<ActiveTabSummary>> GetActiveTabAsync();
        Task<UserTabResponse?> GetOrCreateTabByQrTokenAsync(string qrToken, Guid userId, Guid cpId);
        Task<UserTabResponse?> GetTabByIdAsync(Guid id);
        Task<IEnumerable<UserTabResponse>> GetAllAsync();
        Task<bool> DeleteAsync(Guid id);
        Task<UserDetailTabResponses?> GetTabDetailsAsync(Guid tabId);

        //to controll if user has opened a tab
        Task<bool> HasOpenTabAsync(Guid userId);
        //admin close the tab
        Task<bool> CloseTabAsync(Guid tabId);
        //check if the tab is paid
        Task<bool> IsTabPaidAsync(Guid tabId);

        Task<OpenTabResponse?> OpenTabAsync(string qrToken, Guid userId);
    }
}
