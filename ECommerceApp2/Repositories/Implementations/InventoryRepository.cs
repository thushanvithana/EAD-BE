using MongoDB.Driver;
using System.Threading.Tasks;
using ECommerceApp2.Models;
using ECommerceApp2.Repositories.Interfaces;

namespace ECommerceApp2.Repositories.Implementations
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly IMongoCollection<Inventory> _inventories;

        public InventoryRepository(IMongoDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _inventories = database.GetCollection<Inventory>("Inventories");
        }

        public async Task<Inventory> GetInventoryByProductIdAsync(string productId)
        {
            return await _inventories.Find(i => i.ProductId == productId).FirstOrDefaultAsync();
        }

        public async Task CreateInventoryAsync(Inventory inventory)
        {
            await _inventories.InsertOneAsync(inventory);
        }

        public async Task UpdateInventoryAsync(Inventory inventory)
        {
            await _inventories.ReplaceOneAsync(i => i.ProductId == inventory.ProductId, inventory);
        }

        public async Task DeleteInventoryAsync(string productId)
        {
            await _inventories.DeleteOneAsync(i => i.ProductId == productId);
        }
    }
}
