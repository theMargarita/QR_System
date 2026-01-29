using Application.DTOs.Requests;
using Application.DTOs.Response;

namespace Application.IServices
{
    public interface IUserService
    {
        Task<CreateUserRequest?> CreateUserAsync(CreateUserRequest newUser);
        Task<bool> RemoveUserAsync(int id);
        Task<UserResponse?> GetUserByPhoneAsync(string phoneNumber);
        Task<UserResponse?> GetUserByNameAsync(string name);
        Task<IEnumerable<UserResponse>> GetAllUsersAsync();
        Task<IEnumerable<UserResponse>> GetUsersWithTabsAsync();
        Task<IEnumerable<UserResponse>> GetUsersWithTransactionsAsync();
        Task<UserProfileResponse?> GetUserProfileAsync(int userId);
        Task<IEnumerable<UserResponse>> SearchUsersAsync(string searchTerm);
    }
}
