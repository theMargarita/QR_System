using Domain.Models;

namespace Domain.IRepositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetUserByPhoneAsync(string phoneNumber);
        Task<User?> GetUserByFirstNameAsync(string firstName);
        Task<User?> GetUserByLastNameAsync(string lastName);
        Task<IEnumerable<User>> GetUsersWithTabsAsync();
        Task<IEnumerable<User>> GetUsersWithTabsAndTransactionsAsync();
    }
}