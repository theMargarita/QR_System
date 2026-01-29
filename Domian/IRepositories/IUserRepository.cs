using Domain.Models;

namespace Domain.IRepositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetUserByPhoneAsync(string phoneNumber);
        Task<User?> GetUserByNameAsync(string name);
        Task<IEnumerable<User>> GetUsersWithTabsAsync();
        Task<IEnumerable<User>> GetUsersWithTransactionsAsync();
        Task<User>GetUserProfileAsync(int userId);
        Task<IEnumerable<User>> SearchUsersAsync(string searchTerm);
    }
}