using ECommerceApp2.Models;
using ECommerceApp2.Repositories.Interfaces;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerceApp2.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _users;

        public UserRepository(IMongoDatabase database)
        {
            _users = database.GetCollection<User>("Users");
        }

        public async Task<User> GetUserById(string id)
        {
            return await _users.Find(user => user.Id == id).FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _users.Find(user => user.Email == email).FirstOrDefaultAsync();
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _users.Find(_ => true).ToListAsync();
        }

        public async Task AddUser(User user)
        {
            await _users.InsertOneAsync(user);
        }

        public async Task UpdateUser(User user)
        {
            await _users.ReplaceOneAsync(u => u.Id == user.Id, user);
        }

        public async Task DeleteUser(string id)
        {
            await _users.DeleteOneAsync(user => user.Id == id);
        }

        public async Task<User> GetUserByIdAsync(string userId)
        {
            return await _users.Find(u => u.Id == userId).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<User>> GetActivatedUsers()
        {
            return await _users.Find(user => user.IsActive).ToListAsync();
        }

        public async Task<IEnumerable<User>> GetDeactivatedUsers()
        {
            return await _users.Find(user => !user.IsActive).ToListAsync();
        }


        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _users.Find(u => u.Email == email).FirstOrDefaultAsync();
        }

        // Optional: Specific Method Implementation
        public async Task UpdateUserStatus(string id, bool isActive)
        {
            var update = Builders<User>.Update.Set(u => u.IsActive, isActive);
            await _users.UpdateOneAsync(u => u.Id == id, update);
        }
    }
}
