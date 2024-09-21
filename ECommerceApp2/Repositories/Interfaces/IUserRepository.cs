using ECommerceApp2.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ECommerceApp2.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(string id);
        Task<User> GetUserByUsernameAsync(string username);
        Task CreateUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(string id);
        Task<IEnumerable<User>> GetAllUsersAsync();
    }
}
