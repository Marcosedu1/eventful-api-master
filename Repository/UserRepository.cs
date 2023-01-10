using eventful_api_master.Context;
using eventful_api_master.Interface;
using eventful_api_master.Models;
using Microsoft.EntityFrameworkCore;

namespace eventful_api_master.Repository
{
    public class UserRepository: IUser
    {
        readonly UserContext _dbContext = new();

        public UserRepository(UserContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<User> GetUsers()
        {
            try
            {
                return _dbContext.Users.Where(x => x.active).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task<User> GetUser(int id)
        {
            try
            {
                User? user = await _dbContext.Users.Include(x => x.active).FirstOrDefaultAsync(x => x.Id == id);
                if (user != null)
                {
                    return user;
                }

                throw new ArgumentNullException();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void UpdateUser(User user)
        {
            try
            {
                _dbContext.Entry(user).State = EntityState.Modified;
                _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<User> AddUser(User user)
        {
            try
            {
                _dbContext.Users.Add(user);
                await _dbContext.SaveChangesAsync();
                Console.WriteLine(user);
                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<User> DeleteUser(int id)
        {
            try
            {
                User? user = await _dbContext.Users.FindAsync(id);
                if (user != null)
                {
                    user.active = false;
                    user.changeDate = DateTime.Now;
                    user.changeUser = user.Id;

                    _dbContext.Entry(user).State = EntityState.Modified;
                    _dbContext.SaveChangesAsync();
                    return user;
                }

                throw new ArgumentNullException();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool CheckUser(int id) => _dbContext.Users.Any(x => x.Id == id && x.active);

    }
}
