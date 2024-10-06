// ECommerceApp2.Services.Implementations/InventoryService.cs
/*
 * File Name: InventoryService.cs
 * Author: Thushan Vithana
 * Date: October 6, 2024
 * Description: Implementation of the IInventoryService interface to manage product inventories in the e-commerce application.
 *              This service provides functionality to:
 *              - Get inventory details by product ID.
 *              - Create, update, and delete inventory records.
 *              - Check stock availability and adjust stock levels (increase/decrease).
 *              - Fetch low stock products and their details.
 *              The service utilizes an injected IProductService to retrieve product information and ensure
 *              proper management of inventory levels.
 */
using ECommerceApp2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerceApp2.Repositories.Interfaces;
using ECommerceApp2.Services.Interfaces;
using ECommerceApp2.DTOs;

namespace ECommerceApp2.Services.Implementations
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IProductService _productService; // Inject ProductService

        public InventoryService(IInventoryRepository inventoryRepository, IProductService productService)
        {
            _inventoryRepository = inventoryRepository;
            _productService = productService;
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

        // Implementation of the new method
        public async Task<List<LowStockProductDto>> GetLowStockProductsAsync()
        {
            // Fetch low stock inventories
            var lowStockInventories = await _inventoryRepository.GetLowStockInventoriesAsync();

            if (lowStockInventories == null || lowStockInventories.Count == 0)
                return new List<LowStockProductDto>();

            var lowStockProducts = new List<LowStockProductDto>();

            foreach (var inventory in lowStockInventories)
            {
                // Fetch product details
                var product = await _productService.GetProductByIdAsync(inventory.ProductId);
                if (product != null)
                {
                    lowStockProducts.Add(new LowStockProductDto
                    {
                        ProductId = product.ProductId,
                        Name = product.Name,
                        Category = product.Category,
                        VendorId = product.VendorId,
                        IsActive = product.IsActive,
                        Price = product.Price,
                        Description = product.Description,
                        AvailableStock = inventory.AvailableStock,
                        LowStockThreshold = inventory.LowStockThreshold
                    });
                }
            }

            return lowStockProducts;
        }
    }
}
    

