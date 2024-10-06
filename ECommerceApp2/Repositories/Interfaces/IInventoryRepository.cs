/*
 * File Name: IInventoryRepository.cs
 * Author: Thushan Vithana
 * Date: October 6, 2024
 * Description: Interface for the Inventory repository, defining the operations for managing inventory
 *              associated with products in the e-commerce application.
 *              This interface provides methods to:
 *              - Retrieve inventory by product ID
 *              - Create a new inventory record
 *              - Update an existing inventory record
 *              - Delete an inventory record by product ID
 *              - Retrieve low stock inventories
 *              Implementations of this interface will handle the interaction with the database.
 */

// ECommerceApp2.Repositories.Interfaces/IInventoryRepository.cs
using ECommerceApp2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerceApp2.Repositories.Interfaces
{
    public interface IInventoryRepository
    {
        Task<Inventory> GetInventoryByProductIdAsync(string productId);
        Task CreateInventoryAsync(Inventory inventory);
        Task UpdateInventoryAsync(Inventory inventory);
        Task DeleteInventoryAsync(string productId);

        // New method to get low stock inventories
        Task<List<Inventory>> GetLowStockInventoriesAsync();
    }
}
