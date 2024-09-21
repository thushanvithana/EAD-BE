
using System.Threading.Tasks;
using System.Collections.Generic;
using ECommerceApp2.Models;

namespace ECommerceApp2.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> AuthenticateAsync(string username, string password);
        Task RegisterUserAsync(User user);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(string id);
    }
}
