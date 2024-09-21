using System.Threading.Tasks;
using ECommerceApp2.Models;
using ECommerceApp2.Repositories.Interfaces;
using ECommerceApp2.Services.Interfaces;

namespace ECommerceApp2.Services.Implementations
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryService(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public async Task<Inventory> GetInventoryByProductIdAsync(string productId)
        {
            return await _inventoryRepository.GetInventoryByProductIdAsync(productId);
        }

        public async Task CreateInventoryAsync(Inventory inventory)
        {
            await _inventoryRepository.CreateInventoryAsync(inventory);
        }

        public async Task UpdateInventoryAsync(Inventory inventory)
        {
            await _inventoryRepository.UpdateInventoryAsync(inventory);
        }

        public async Task DeleteInventoryAsync(string productId)
        {
            await _inventoryRepository.DeleteInventoryAsync(productId);
        }

        public async Task<bool> IsStockAvailableAsync(string productId, int quantity)
        {
            var inventory = await _inventoryRepository.GetInventoryByProductIdAsync(productId);
            return inventory != null && inventory.AvailableStock >= quantity;
        }

        public async Task DecreaseStockAsync(string productId, int quantity)
        {
            var inventory = await _inventoryRepository.GetInventoryByProductIdAsync(productId);
            if (inventory != null && inventory.AvailableStock >= quantity)
            {
                inventory.AvailableStock -= quantity;
                await _inventoryRepository.UpdateInventoryAsync(inventory);
            }
            else
            {
                throw new System.Exception("Insufficient stock.");
            }
        }

        public async Task IncreaseStockAsync(string productId, int quantity)
        {
            var inventory = await _inventoryRepository.GetInventoryByProductIdAsync(productId);
            if (inventory != null)
            {
                inventory.AvailableStock += quantity;
                await _inventoryRepository.UpdateInventoryAsync(inventory);
            }
            else
            {
                throw new System.Exception("Inventory not found.");
            }
        }
    }
}
