// ECommerceApp2.Services.Interfaces/IInventoryService.cs
using ECommerceApp2.DTOs;
using ECommerceApp2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerceApp2.Services.Interfaces
{
    public interface IInventoryService
    {
        Task<Inventory> GetInventoryByProductIdAsync(string productId);
        Task CreateInventoryAsync(Inventory inventory);
        Task UpdateInventoryAsync(Inventory inventory);
        Task DeleteInventoryAsync(string productId);
        Task<bool> IsStockAvailableAsync(string productId, int quantity);
        Task DecreaseStockAsync(string productId, int quantity);
        Task IncreaseStockAsync(string productId, int quantity);

        // New method to get low stock inventories
        Task<List<LowStockProductDto>> GetLowStockProductsAsync();
    }
}
