
using MongoDB.Driver;
using System.Threading.Tasks;
using ECommerceApp2.Models;
using ECommerceApp2.Repositories.Interfaces;

namespace ECommerceApp2.Repositories.Implementations
{
    public class CartRepository : ICartRepository
    {
        private readonly IMongoCollection<Cart> _carts;

        public CartRepository(IMongoDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _carts = database.GetCollection<Cart>("Carts");
        }

        public async Task<Cart> GetCartByUserIdAsync(string userId)
        {
            return await _carts.Find(c => c.UserId == userId).FirstOrDefaultAsync();
        }

        public async Task CreateCartAsync(Cart cart)
        {
            await _carts.InsertOneAsync(cart);
        }

        public async Task UpdateCartAsync(Cart cart)
        {
            await _carts.ReplaceOneAsync(c => c.Id == cart.Id, cart);
        }

        public async Task DeleteCartAsync(string cartId)
        {
            await _carts.DeleteOneAsync(c => c.Id == cartId);
        }
    }
}
