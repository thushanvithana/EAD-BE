using System.Threading.Tasks;
using System.Linq;
using ECommerceApp2.Models;
using ECommerceApp2.Repositories.Interfaces;
using ECommerceApp2.Services.Interfaces;

namespace ECommerceApp2.Services.Implementations
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<Cart> GetCartByUserIdAsync(string userId)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            if (cart == null)
            {
                cart = new Cart { UserId = userId };
                await _cartRepository.CreateCartAsync(cart);
            }
            return cart;
        }

        public async Task AddItemToCartAsync(string userId, CartItem item)
        {
            var cart = await GetCartByUserIdAsync(userId);

            var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == item.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                cart.Items.Add(item);
            }

            await _cartRepository.UpdateCartAsync(cart);
        }

        public async Task RemoveItemFromCartAsync(string userId, string productId)
        {
            var cart = await GetCartByUserIdAsync(userId);
            cart.Items.RemoveAll(i => i.ProductId == productId);
            await _cartRepository.UpdateCartAsync(cart);
        }

        public async Task ClearCartAsync(string userId)
        {
            var cart = await GetCartByUserIdAsync(userId);
            cart.Items.Clear();
            await _cartRepository.UpdateCartAsync(cart);
        }
    }
}
