using System.Threading.Tasks;
using System.Collections.Generic;
using ECommerceApp2.Models;

namespace ECommerceApp2.Services.Interfaces
{
    public interface IOrderService
    {
        Task CreateOrderAsync(Order order);
        Task<Order> GetOrderByIdAsync(string orderId);
        Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(string customerId);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task UpdateOrderAsync(Order order);
        Task CancelOrderAsync(string orderId, string cancellationReason);
    }
}
