using ECommerceApp2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerceApp2.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> Register(User user);
        Task<User> Login(string email, string password);
        Task<User> GetUserById(string id);
        Task<List<User>> GetAllUsers();
        Task UpdateUser(User user);
        Task DeleteUser(string id);

        // New Methods
        Task ActivateUser(string id);
        Task DeactivateUser(string id);
    }
}
