using System.Threading.Tasks;
using System.Collections.Generic;
using ECommerceApp2.Models;

namespace ECommerceApp2.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> GetOrderByIdAsync(string orderId);
        Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(string customerId);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task CreateOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(string orderId);
    }
}
