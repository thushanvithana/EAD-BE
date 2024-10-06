/*
 * File Name: CartController.cs
 * Author: Thushan Vithana
 * Date: October 6, 2024
 * Description: ASP.NET Controller that handles HTTP requests related to the shopping cart
 *              operations, including retrieving the cart by user ID, adding items to the cart,
 *              removing items, and clearing the entire cart. Utilizes dependency injection 
 *              to access the ICartService for business logic.
 */

using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;
using ECommerceApp2.Models;
using ECommerceApp2.Services.Interfaces;

namespace ECommerceApp2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        // Dependency Injection
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        // GET api/cart/{userId}
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCart(string userId)
        {
            var cart = await _cartService.GetCartByUserIdAsync(userId);
            return Ok(cart);
        }

        // POST api/cart/{userId}/items
        [HttpPost("{userId}/items")]
        public async Task<IActionResult> AddItemToCart(string userId, [FromBody] CartItem item)
        {
            await _cartService.AddItemToCartAsync(userId, item);
            return Ok("Item added to cart.");
        }

        // DELETE api/cart/{userId}/items/{productId}
        [HttpDelete("{userId}/items/{productId}")]
        public async Task<IActionResult> RemoveItemFromCart(string userId, string productId)
        {
            await _cartService.RemoveItemFromCartAsync(userId, productId);
            return Ok("Item removed from cart.");
        }

        // DELETE api/cart/{userId}
        [HttpDelete("{userId}")]
        public async Task<IActionResult> ClearCart(string userId)
        {
            await _cartService.ClearCartAsync(userId);
            return Ok("Cart cleared.");
        }
    }
}
