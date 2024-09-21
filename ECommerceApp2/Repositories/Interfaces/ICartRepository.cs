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
