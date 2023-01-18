using eventful_api_master.Context;
using eventful_api_master.Interface;
using eventful_api_master.Models;
using Microsoft.EntityFrameworkCore;

namespace eventful_api_master.Repository
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        readonly DatabaseContext _dbContext = new();

        public AuthenticationRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User?> SignIn(string email, string password)
        {
            try
            {
                User user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email && x.Password == password && x.Active);
                return user;
            }
            catch (Exception)
            {                        
                throw;
            }
        }
    }
}
