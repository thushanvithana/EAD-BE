
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
