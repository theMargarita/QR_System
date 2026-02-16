using Application.DTOs.UserFolder.Request;
using Application.DTOs.UserFolder.Response;
using Application.IServices;
using Domain.Models;
using Infrastructure.Data;
using Infrastructure.Extensions;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly QrDbContext _context;
        private readonly ILogger _logger;
        public UserService(QrDbContext contect, ILogger<UserService> logger)
        {
            _context = contect;
            _logger = logger;
        }

        public async Task<UserResponse?> CreateUserAsync(CreateUserRequest newUser)
        {
            var user = new User
            {
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                PhoneNumber = newUser.PhoneNumber,
                CreatedAt = newUser.CreatedAt.UtcDateTime
            };

            if(user == null)
            {
                _logger.LogError("Failed to create user");
                return null;
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Created new user with ID {user.Id}");

            return  UserResponse.FromUser(user);

        }
        
        //how does it work to remove a user with a guid id?
        public async Task<bool> RemoveUserAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if(user == null)
            {
                _logger.LogWarning($"User with ID {id} not found for deletion.");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Deleted user with ID {id}.");
            return true;
        }
        public async Task<UserResponse> GetUserByIdAsync(Guid id)
        {
           var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                _logger.LogWarning($"User with ID {id} not found.");
            }
            return UserResponse.FromUser(user);
        }

        public async Task<IEnumerable<UserResponse>> GetAllUsersAsync()
        {
            var user = _context.Users;

            return user.Select(u => UserResponse.FromUser(u)).ToList();
        }

        //must test to se if it works
        public async Task<IEnumerable<UserResponse>> SearchUsersAsync(string searchTerm)
        {
            var user = _context.Users.Search(searchTerm);

            return user.Select(u => UserResponse.FromUser(u)).ToList();
        }

        //cannot update - wiill take of this one lateer
        public async Task<UserResponse> UpdateUserAsync(Guid id, CreateUserRequest updatedUser)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                _logger.LogWarning($"User with ID {id} not found for update.");
            }
            user = new User
            {
                FirstName = updatedUser.FirstName,
                LastName = updatedUser.LastName,
                PhoneNumber = updatedUser.PhoneNumber,
            };
             _context.Users.Update(user);
             await _context.SaveChangesAsync();

             _logger.LogInformation($"Updated user with ID {id}.");
             return UserResponse.FromUser(user);
        }

        //will implement this later
        public Task<UserProfileResponse?> GetUserProfileAsync(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
