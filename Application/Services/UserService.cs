using Application.DTOs.Requests;
using Application.DTOs.Response;
using Application.DTOs.Summary;
using Application.IServices;
//using AutoMapper;
using Domain.Models;
using Domian.IRepoistories;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
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
            //var user = _mapper.Map<User>(newUser);
            //await _unitOfWork.Users.CreateAsync(user);

            //return _mapper.Map<CreateUserRequest>(user);
            return  new CreateUserRequest
            {
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                PhoneNumber = newUser.PhoneNumber
            };
        }
        public async Task<bool> RemoveUserAsync(int id)
        {
            var user = _unitOfWork.Users.GetByIdAsync(id);
            if (user == null)
            {
                return false;
            }
            await _unitOfWork.Users.DeleteAsync(id);
            return true;
        }

        public async Task<IEnumerable<UserResponse>> GetAllUsersAsync()
        {
            //return await _unitOfWork.Users.GetAllAsync();
            //return _mapper.Map<IEnumerable<UserResponse>>(users);
            //return await _unitOfWork.Users.GetAllUsersAsync();
            return _unitOfWork.Users.FindAsync(u => true).Result
                .Select(u => new UserResponse
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    PhoneNumber = u.PhoneNumber,
                    HasPaid = u.Payments != null && u.Payments.Any(p => p.Amount > 0)
                });
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
        public async Task<UserProfileResponse?> GetUserProfileAsync(int userId)
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
