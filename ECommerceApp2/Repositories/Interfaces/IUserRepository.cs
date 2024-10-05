using ECommerceApp2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerceApp2.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserById(string id);
        Task<User> GetUserByIdAsync(string userId);
        Task<User> GetUserByEmail(string email);
        Task<List<User>> GetAllUsers();
        Task AddUser(User user);
        Task UpdateUser(User user);
        Task DeleteUser(string id);

        // Optional: Specific Methods
        Task UpdateUserStatus(string id, bool isActive);
        Task<User> GetUserByEmailAsync(string email);

        Task<IEnumerable<User>> GetActivatedUsers();
        Task<IEnumerable<User>> GetDeactivatedUsers();
    }
}
