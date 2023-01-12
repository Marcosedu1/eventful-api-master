using eventful_api_master.Models;

namespace eventful_api_master.Interface
{
    public interface IUserRepository
    {
        public List<User> GetUsers();
        public Task<User?> GetUser(int id);
        public Task<User> AddUser(User user);
        public Task UpdateUser(User user);
        public Task<User> DeleteUser(int id);
        public bool CheckUser(int id);
    }
}
