using eventful_api_master.Models;

namespace eventful_api_master.Interface
{
    public interface IAuthenticationRepository
    {
        public Task<User?> SignIn(string email, string password);
    }
}
