
/*
 * File Name: CartRepository.cs
 * Author: Thushan Vithana
 * Date: October 6, 2024
 * Description: Implementation of the ICartRepository interface for managing shopping carts in the MongoDB database.
 *              This repository provides methods to:
 *              - Retrieve a cart by user ID
 *              - Create a new cart
 *              - Update an existing cart
 *              - Delete a cart by its ID
 *              Uses MongoDB.Driver to interact with the "Carts" collection in the specified database.
 */
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
