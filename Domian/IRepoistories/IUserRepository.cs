using Domain.Models;

namespace Domain.IRepositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetUserByPhoneAsync(string phoneNumber);
        Task<User?> GetUserByUsernameAsync(string username);
        Task<IEnumerable<User>> GetUsersWithTabsAsync();
        Task<IEnumerable<User>> GetUsersWithTabsAndTransactionsAsync();
    }
}