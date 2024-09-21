using ECommerceApp2.Models;
using System.Threading.Tasks;

namespace ECommerceApp2.Repositories.Interfaces
{
    public interface IInventoryRepository
    {
        Task<Inventory> GetInventoryByProductIdAsync(string productId);
        Task CreateInventoryAsync(Inventory inventory);
        Task UpdateInventoryAsync(Inventory inventory);
        Task DeleteInventoryAsync(string productId);
    }
}
