using Application.DTOs.Requests;
using Application.DTOs.Response;

namespace Application.IServices
{
    public interface IUserService
    {
        Task<CreateUserRequest?> CreateUserAsync(CreateUserRequest newUser);
        Task<bool> RemoveUserAsync(Guid id);
        Task<UserResponse> GetUserByIdAsync(Guid id);
        Task<UserResponse?> GetUserByPhoneAsync(string phoneNumber);
        Task<UserResponse?> GetUserByNameAsync(string name);
        Task<IEnumerable<UserResponse>> GetAllUsersAsync();
        Task<IEnumerable<UserResponse>> GetUsersWithTabsAsync();
        Task<IEnumerable<UserResponse>> GetUsersWithTransactionsAsync();
        Task<UserProfileResponse?> GetUserProfileAsync(Guid userId);
        Task<IEnumerable<UserResponse>> SearchUsersAsync(string searchTerm);
    }
}
