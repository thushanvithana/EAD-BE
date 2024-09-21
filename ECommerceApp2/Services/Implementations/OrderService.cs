using System.Threading.Tasks;
using System.Collections.Generic;
using ECommerceApp2.Models;
using ECommerceApp2.Repositories.Interfaces;
using ECommerceApp2.Services.Interfaces;

namespace ECommerceApp2.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        // Inject other repositories/services as needed (e.g., InventoryRepository)

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }


        public async Task MarkItemAsReadyForDelivery(string orderId, string productId, string vendorId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null)
                throw new KeyNotFoundException("Order not found.");

            var item = order.Items.Find(i => i.ProductId == productId && i.VendorId == vendorId);
            if (item == null)
                throw new KeyNotFoundException("Order item not found for this vendor.");

            item.IsDelivered = true;

            // Check if all items are delivered
            if (order.Items.TrueForAll(i => i.IsDelivered))
            {
                order.Status = OrderStatus.Delivered;
            }
            else if (order.Items.Exists(i => i.IsDelivered))
            {
                order.Status = OrderStatus.PartiallyDelivered;
            }

            await _orderRepository.UpdateOrderAsync(order);
        }




        public async Task CreateOrderAsync(Order order)
        {
            // Business logic for creating an order
            await _orderRepository.CreateOrderAsync(order);
        }

        public async Task<Order> GetOrderByIdAsync(string orderId)
        {
            return await _orderRepository.GetOrderByIdAsync(orderId);
        }

        public async Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(string customerId)
        {
            return await _orderRepository.GetOrdersByCustomerIdAsync(customerId);
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllOrdersAsync();
        }

        public async Task UpdateOrderAsync(Order order)
        {
            // Business logic for updating an order
            await _orderRepository.UpdateOrderAsync(order);
        }

        public async Task CancelOrderAsync(string orderId, string cancellationReason)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null)
                throw new KeyNotFoundException("Order not found.");

            if (order.Status == OrderStatus.Processing)
            {
                order.Status = OrderStatus.Cancelled;
                order.CancellationReason = cancellationReason;
                await _orderRepository.UpdateOrderAsync(order);
            }
            else
            {
                throw new InvalidOperationException("Order cannot be cancelled at this stage.");
            }
        }
    }
}
