using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceApp2.Models;
using ECommerceApp2.Repositories.Interfaces;
using ECommerceApp2.Services.Interfaces;

namespace ECommerceApp2.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public async Task CreateOrderAsync(Order order)
        {
            // Existing logic to create order
            await _orderRepository.CreateOrderAsync(order);
        }

        public async Task<OrderDto> GetOrderByIdAsync(string orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null)
                return null;

            var totalValue = await CalculateTotalValueAsync(order);

            return MapToOrderDto(order, totalValue);
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByCustomerIdAsync(string customerId)
        {
            var orders = await _orderRepository.GetOrdersByCustomerIdAsync(customerId);
            var orderDtos = new List<OrderDto>();

            foreach (var order in orders)
            {
                var totalValue = await CalculateTotalValueAsync(order);
                orderDtos.Add(MapToOrderDto(order, totalValue));
            }

            return orderDtos;
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllOrdersAsync();
            var orderDtos = new List<OrderDto>();

            foreach (var order in orders)
            {
                var totalValue = await CalculateTotalValueAsync(order);
                orderDtos.Add(MapToOrderDto(order, totalValue));
            }

            return orderDtos;
        }

        public async Task UpdateOrderAsync(Order order)
        {
            // Existing logic to update order
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

        public async Task MarkItemAsReadyForDelivery(string orderId, string productId, string vendorId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null)
                throw new KeyNotFoundException("Order not found.");

            var item = order.Items.Find(i => i.ProductId == productId && i.VendorId == vendorId);
            if (item == null)
                throw new KeyNotFoundException("Order item not found for this vendor.");

            item.IsDelivered = true;

            // Update order status based on delivery
            if (order.Items.All(i => i.IsDelivered))
            {
                order.Status = OrderStatus.Delivered;
            }
            else if (order.Items.Any(i => i.IsDelivered))
            {
                order.Status = OrderStatus.PartiallyDelivered;
            }

            await _orderRepository.UpdateOrderAsync(order);
        }

        // Helper method to calculate total value
        private async Task<decimal> CalculateTotalValueAsync(Order order)
        {
            decimal total = 0m;

            foreach (var item in order.Items)
            {
                var product = await _productRepository.GetProductByIdAsync(item.ProductId);
                if (product != null)
                {
                    total += product.Price * item.Quantity;
                }
                else
                {
                    // Handle case where product is not found
                    // You might want to log this or throw an exception
                }
            }

            return total;
        }

        // Helper method to map Order to OrderDto
        private OrderDto MapToOrderDto(Order order, decimal totalValue)
        {
            return new OrderDto
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                Items = order.Items.Select(i => new OrderItemDto
                {
                    ProductId = i.ProductId,
                    VendorId = i.VendorId,
                    Quantity = i.Quantity,
                    IsDelivered = i.IsDelivered
                }).ToList(),
                Status = order.Status,
                CancellationReason = order.CancellationReason,
                Note = order.Note,
                CreatedAt = order.CreatedAt,
                TotalValue = totalValue
            };
        }
    }
}
