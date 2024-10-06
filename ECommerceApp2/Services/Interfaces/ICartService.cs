/*
 * File Name: CartService.cs
 * Author: Thushan Vithana
 * Date: October 6, 2024
 * Description: Implementation of the ICartService interface to manage shopping carts in the e-commerce application.
 *              This service provides functionality to:
 *              - Retrieve the cart for a specific user by user ID.
 *              - Add items to the user's cart.
 *              - Remove specific items from the user's cart based on the product ID.
 *              - Clear all items from the user's cart.
 *              The service interacts with the Cart model to ensure proper cart management and 
 *              utilizes asynchronous operations for improved performance and responsiveness.
 */
using ECommerceApp2.Models;
using System.Threading.Tasks;

namespace ECommerceApp2.Services.Interfaces
{
    public interface ICartService
    {
        Task<Cart> GetCartByUserIdAsync(string userId);
        Task AddItemToCartAsync(string userId, CartItem item);
        Task RemoveItemFromCartAsync(string userId, string productId);
        Task ClearCartAsync(string userId);
    }
}
