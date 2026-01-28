using Domain.IRepositories;
using Domain.Models;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(QrDbContext context) : base(context)
        {
        }
        public async Task<User?> GetUserByPhoneAsync(string phoneNumber)
        {
            var user = await _dbSet.FindAsync(phoneNumber);
            if(user == null)
            {
                return null;
            }   

            return user;
        }

        public async Task<User?> GetUserByFirstNameAsync(string firstName)
        {
            var user = _dbSet.FirstOrDefault(u => u.FirstName == firstName);
            if( user.FirstName == firstName)
            {
                return user; ;
            }
            return null;
        }

        public async Task<User?> GetUserByLastNameAsync(string lastName)
        {
            var user =  _dbSet.FirstOrDefault( u => u.LastName == lastName);
            if( user.LastName == lastName)
            {
                return user; ;
            }
            return null;
        }

        public Task<IEnumerable<User>> GetUsersWithTabsAndTransactionsAsync()
        {
            //var users = await _dbSet.
            throw new NotImplementedException();


        }

        public Task<IEnumerable<User>> GetUsersWithTabsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
