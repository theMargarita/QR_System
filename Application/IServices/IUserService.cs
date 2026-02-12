using Application.DTOs.UserFolder.Request;
using Application.DTOs.UserFolder.Response;

namespace Application.IServices
{
    public interface IUserService
    {
        Task<UserResponse?> CreateUserAsync(CreateUserRequest newUser);
        Task<bool> RemoveUserAsync(Guid id);
        Task<UserResponse> UpdateUserAsync(Guid id, CreateUserRequest updatedUser);
        Task<UserResponse> GetUserByIdAsync(Guid id);
        Task<IEnumerable<UserResponse>> GetAllUsersAsync();
        Task<UserProfileResponse?> GetUserProfileAsync(Guid userId);
        Task<IEnumerable<UserResponse>> SearchUsersAsync(string searchTerm);
    }
}
