using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerceApp2.Models;

namespace ECommerceApp2.Services.Interfaces
{
    public interface IOrderService
    {
        Task CreateOrderAsync(Order order);
        Task<OrderDto> GetOrderByIdAsync(string orderId);
        Task<IEnumerable<OrderDto>> GetOrdersByCustomerIdAsync(string customerId);
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
        Task UpdateOrderAsync(Order order);
        Task CancelOrderAsync(string orderId, string cancellationReason);
        Task MarkItemAsReadyForDelivery(string orderId, string productId, string vendorId);
        Task<OrderWithDetailsDto> GetOrderWithDetailsByIdAsync(string orderId);
        Task<Order> CreateOrderWithEmailAsync(CreateOrderRequest request);

        Task<IEnumerable<OrderWithDetailsDto>> GetAllOrdersWithDetailsAsync();



    }
}
