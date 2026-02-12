//using Application.DTOs.UserTabFolder.AdminTabFolder;
//using Application.DTOs.UserTabFolder.Resonse;
//using Domain.Models;

//namespace Application.IServices
//{
//    public interface IUserTabService
//    {
//        Task<IEnumerable<ActiveTabSummary>> GetActiveTabAsync();
//        Task<UserTabResponse?> GetByIdAsync(int id);
//        Task<IEnumerable<UserTabResponse>> GetAllAsync();
//        Task<UserTabResponse> CreateAsync(UserTab tab);
//        Task<UserTabResponse> UpdateAsync(UserTab tab);
//        Task<bool> DeleteAsync(int id);
//        Task<UserDetailTabResponses?> GetTabDetailsAsync(int tabId);

//        //to controll if user has opened a tab
//        Task<bool> HasOpenTabAsync(int userId);
//        //admin close the tab
//        Task<bool> CloseTabAsync(int tabId);
//        //check if the tab is paid
//        Task<bool> IsTabPaidAsync(int tabId);
//    }
//}
