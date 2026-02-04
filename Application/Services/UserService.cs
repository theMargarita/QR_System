using Application.DTOs.Requests;
using Application.DTOs.Response;
using Application.DTOs.Summary;
using Application.IServices;
using Domain.IRepositories;
using Domain.Models;


using Microsoft.Extensions.Logging;

namespace Application.Services
{
    //DO NOT FORGET SAVECHANGES!
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;
        public UserService(IUnitOfWork unitOfWork, ILogger<UserService> logger)
        {
            _unitOfWork = unitOfWork;
            //_mapper = mapper;
            _logger = logger;
        }

        public async Task<CreateUserRequest?> CreateUserAsync(CreateUserRequest newUser)
        {
            var user = new User
            {
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                PhoneNumber = newUser.PhoneNumber,
                CreatedAt = newUser.CreatedAt.UtcDateTime
            };

            await _unitOfWork.Users.CreateAsync(user);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("Created new user with ID {UserId}", user.Id);

            return new CreateUserRequest
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                CreatedAt = user.CreatedAt
            };

            //var user = _mapper.Map<User>(newUser);
            //await _unitOfWork.Users.CreateAsync(user);
            //await _unitOfWork.SaveChangesAsync();

            //return _mapper.Map<CreateUserRequest>(user);
        }
        public async Task<bool> RemoveUserAsync(Guid id)
        {
            var user = _unitOfWork.Users.GetByIdAsync(id);
            if (user == null)
            {
                return false;
            }
            await _unitOfWork.Users.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
        public async Task<UserResponse> GetUserByIdAsync(Guid id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user == null)
            {
                return null;
            }
            return new UserResponse
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                HasPaid = user.Payments != null && user.Payments.Any(p => p.Amount > 0)
            };
        }

        public async Task<IEnumerable<UserResponse>> GetAllUsersAsync()
        {
            //return await _unitOfWork.Users.GetAllAsync();
            //return _mapper.Map<IEnumerable<UserResponse>>(users);
            //return await _unitOfWork.Users.GetAllUsersAsync();
            var user =  _unitOfWork.Users.FindAsync(u => true).Result
                .Select(u => new UserResponse
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    PhoneNumber = u.PhoneNumber,
                    HasPaid = u.Payments != null && u.Payments.Any(p => p.Amount > 0)
                });
            await _unitOfWork.Users.GetAllAsync();
            await _unitOfWork.SaveChangesAsync();
            return user;
        }

        public async Task<UserResponse?> GetUserByNameAsync(string name)
        {
            //var user = await _unitOfWork.Users.GetUserByNameAsync(name);
            //if(user == null)
            //{
            //    return null;
            //}

            //return _mapper.Map<UserResponse>(user);

            var user = _unitOfWork.Users.FindAsync(u => u.FirstName == name || u.LastName == name).Result
                .Select(u => new UserResponse
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    PhoneNumber = u.PhoneNumber,
                    HasPaid = u.Payments != null && u.Payments.Any(p => p.Amount > 0)
                })
                .FirstOrDefault();
            await _unitOfWork.Users.GetAllAsync();
            await _unitOfWork.SaveChangesAsync();

            return user;
        }

        public async Task<UserResponse?> GetUserByPhoneAsync(string phoneNumber)
        {
            //var user = await _unitOfWork.Users.GetUserByPhoneAsync(phoneNumber);
            //if (user == null)
            //{
            //    return null;
            //}

            //return _mapper.Map<UserResponse>(user);

            return _unitOfWork.Users.FindAsync(u => u.PhoneNumber == phoneNumber).Result
                .Select(u => new UserResponse
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    PhoneNumber = u.PhoneNumber,
                    HasPaid = u.Payments != null && u.Payments.Any(p => p.Amount > 0)
                })
                .FirstOrDefault();
        }

        //maybe should also have showUSerProfile? 
        public async Task<UserProfileResponse?> GetUserProfileAsync(Guid userId)
        {
            //var user = await _unitOfWork.Users.GetUserProfileAsync(userId);
            //if (user == null)
            //{
            //    return null;
            //}
            //return _mapper.Map<UserProfileResponse>(user);

            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (user == null)
            {
                return null;
            }

            return new UserProfileResponse
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                CreatedAt = user.CreatedAt,
                ActiveTabs = user.Tabs != null ? user.Tabs.Select(t => new UserTabSummaryResponse
                {
                    Id = t.Id,


                }).ToList() : new List<UserTabSummaryResponse>()
            };
        }
        public async Task<IEnumerable<UserResponse>> GetUsersWithTabsAsync()
        {
            //var users = await _unitOfWork.Users.GetUsersWithTabsAsync();
            //return _mapper.Map<IEnumerable<UserResponse>>(users);

            var users = await _unitOfWork.Users.GetUsersWithTabsAsync();
            return users.Select(u => new UserResponse
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                PhoneNumber = u.PhoneNumber,
                HasPaid = u.Payments != null && u.Payments.Any(p => p.Amount > 0)
            });
        }

        public async Task<IEnumerable<UserResponse>> GetUsersWithTransactionsAsync()
        {
            //var users = await _unitOfWork.Users.GetUsersWithTransactionsAsync();
            //return _mapper.Map<IEnumerable<UserResponse>>(users);

            var users = await _unitOfWork.Users.GetUsersWithTransactionsAsync();
            return users.Select(u => new UserResponse
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                PhoneNumber = u.PhoneNumber,
                HasPaid = u.Payments != null && u.Payments.Any(p => p.Amount > 0)
            });
        }


        public async Task<IEnumerable<UserResponse>> SearchUsersAsync(string searchTerm)
        {
            var users = await _unitOfWork.Users.SearchUsersAsync(searchTerm);
            return users.Select(u => new UserResponse { 
                Id = u.Id, 
                FirstName = u.FirstName, 
                LastName = u.LastName, 
                PhoneNumber = u.PhoneNumber, 
                HasPaid = u.Payments != null && u.Payments
                .Any(p => p.Amount > 0) });
        }
       
    }
}
