// ECommerceApp.Controllers/InventoryController.cs
/*
 * File Name: InventoryController.cs
 * Author: Thushan Vithana
 * Date: October 6, 2024
 * Description: ASP.NET Controller that manages inventory-related operations, including
 *              creating, updating, retrieving, and deleting inventory items. The controller
 *              also provides an endpoint for retrieving products that are below the low stock 
 *              threshold, ensuring effective inventory management.
 */


using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ECommerceApp2.Models;
using ECommerceApp2.Services.Interfaces;
using System.Collections.Generic;

namespace ECommerceApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;

        // Dependency Injection
        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        // Existing endpoints...

        // GET api/inventory/{productId}
        [HttpGet("{productId}")]
        public async Task<IActionResult> GetInventory(string productId)
        {
            var inventory = await _inventoryService.GetInventoryByProductIdAsync(productId);
            if (inventory == null)
                return NotFound();

            return Ok(inventory);
        }

        // POST api/inventory
        [HttpPost]
        public async Task<IActionResult> CreateInventory(Inventory inventory)
        {
            await _inventoryService.CreateInventoryAsync(inventory);
            return Ok("Inventory created successfully.");
        }

        // PUT api/inventory/{productId}
        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateInventory(string productId, Inventory updatedInventory)
        {
            var inventory = await _inventoryService.GetInventoryByProductIdAsync(productId);
            if (inventory == null)
                return NotFound();

            updatedInventory.ProductId = productId;
            await _inventoryService.UpdateInventoryAsync(updatedInventory);
            return Ok("Inventory updated successfully.");
        }

        // DELETE api/inventory/{productId}
        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteInventory(string productId)
        {
            await _inventoryService.DeleteInventoryAsync(productId);
            return Ok("Inventory deleted successfully.");
        }

        // New endpoint to get low stock inventories
        // GET api/inventory/lowstock
        [HttpGet("lowstock/details")]
        public async Task<IActionResult> GetLowStockProductsWithDetails()
        {
            var lowStockProducts = await _inventoryService.GetLowStockProductsAsync();

            if (lowStockProducts == null || lowStockProducts.Count == 0)
                return Ok("No products are currently below the low stock threshold.");

            return Ok(lowStockProducts);
        }
    }
}
