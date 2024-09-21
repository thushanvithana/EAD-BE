using ECommerceApp2.Models;
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
    }
}
