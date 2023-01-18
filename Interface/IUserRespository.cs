using eventful_api_master.Models;

namespace eventful_api_master.Interface
{
    public interface IUserRepository
    {
        public List<User> GetUsers();
        public Task<User?> GetUser(int id);
        public Task<User> AddUser(User user);
        public Task UpdateUser(User user);
        public Task<User> DeleteUser(User user);
        public Task<User?> Validate(string email, string password);
        public bool CheckUser(int id);
    }
}
