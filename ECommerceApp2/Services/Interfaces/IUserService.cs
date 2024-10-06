using ECommerceApp2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static ECommerceApp2.Controllers.UserController;

namespace ECommerceApp2.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> Register(User user);
        Task<User> Login(string email, string password);
        Task<User> GetUserById(string id);
        Task<List<User>> GetAllUsers();
        Task DeleteUser(string id);
        Task ActivateUser(string id);
        Task DeactivateUser(string id);
        Task<IEnumerable<User>> GetActivatedUsers(); 
        Task<IEnumerable<User>> GetDeactivatedUsers();




        Task UpdateUser(string id, UpdateUserRequest updateRequest);
    }
}
