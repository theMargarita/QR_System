using Application.DTOs.UserFolder.Request;
using Application.DTOs.UserFolder.Response;

namespace Application.IServices
{
    public interface IUserService
    {
        Task<CreateUserRequest?> CreateUserAsync(CreateUserRequest newUser);
        Task<bool> RemoveUserAsync(int id);
        Task<UserResponse> GetUserByIdAsync(int id);
        Task<UserResponse?> GetUserByPhoneAsync(string phoneNumber);
        Task<UserResponse?> GetUserByNameAsync(string name);
        Task<IEnumerable<UserResponse>> GetAllUsersAsync();
        Task<IEnumerable<UserResponse>> GetUsersWithTabsAsync();
        Task<IEnumerable<UserResponse>> GetUsersWithTransactionsAsync();
        Task<UserProfileResponse?> GetUserProfileAsync(int userId);
        Task<IEnumerable<UserResponse>> SearchUsersAsync(string searchTerm);
    }
}
