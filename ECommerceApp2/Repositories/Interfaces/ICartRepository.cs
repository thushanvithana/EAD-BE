/*
 * File Name: ICartRepository.cs
 * Author: Thushan Vithana
 * Date: October 6, 2024
 * Description: Interface for the Cart repository, defining the operations for managing shopping carts
 *              associated with users in the e-commerce application.
 *              This interface provides methods to:
 *              - Retrieve a cart by user ID
 *              - Create a new cart
 *              - Update an existing cart
 *              - Delete a cart by its ID
 *              Implementations of this interface will handle the interaction with the database.
 */
using ECommerceApp2.Models;
using System.Threading.Tasks;

namespace ECommerceApp2.Repositories.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart> GetCartByUserIdAsync(string userId);
        Task CreateCartAsync(Cart cart);
        Task UpdateCartAsync(Cart cart);
        Task DeleteCartAsync(string cartId);
    }
}
