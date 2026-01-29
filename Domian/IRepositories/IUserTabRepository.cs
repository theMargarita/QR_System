using Domain.Models;

namespace Domain.IRepositories
{
    public interface IUserTabRepository : IRepository<UserTab>
    {
        //find tabs by related data
        Task<UserTab?> GetTabWithDetailsAsync(int id);

        //find tab based on status/user/context
        Task<IEnumerable<UserTab>> GetActiveTabsByContextIdAsync(int contextId);
        Task<IEnumerable<UserTab>> GetTabsByUserIdAsync(int userId);
        Task<IEnumerable<UserTab>> GetOpenTabsAsync();
        Task<IEnumerable<UserTab>> GetOpenTabsByContextIdAsync(int contextId); // to get specific open tabs in a context


        //check if user have opened a tab 
        Task<UserTab?> GetActiveTabByUserIdAsync(int userId); 
        Task<bool> HasOpenTabAsync(int userId);


        //calculation
        Task<decimal> GetTabTotalAsync(int tabId); 
        Task<decimal> GetTabBalanceAsync(int tabId);

    }
}
