using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ECommerceApp2.Models;
using ECommerceApp2.Services.Interfaces;

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
    }
}
