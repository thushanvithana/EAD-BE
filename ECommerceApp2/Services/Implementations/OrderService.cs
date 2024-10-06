using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceApp2.Models;
using ECommerceApp2.Repositories.Implementations;
using ECommerceApp2.Repositories.Interfaces;
using ECommerceApp2.Services.Interfaces;

namespace ECommerceApp2.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;  // Add this field

        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository, IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;  // Assign user repository
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


        public async Task<Order> CreateOrderWithEmailAsync(CreateOrderRequest request)
        {
            // Find user by email
            var user = await _userRepository.GetUserByEmailAsync(request.CustomerEmail);
            if (user == null)
                throw new KeyNotFoundException("Customer not found with the given email.");

            // Create a new order
            var order = new Order
            {
                CustomerId = user.Id, // Set CustomerId from User
                Items = request.Items.Select(item => new OrderItem
                {
                    ProductId = item.ProductId,
                    VendorId = item.VendorId,
                    Quantity = item.Quantity,
                    IsDelivered = false, // Default to not delivered
                }).ToList(),
                Note = request.Note
            };

            // Calculate the total value
            order.TotalValue = await CalculateTotalValueAsync(order);

            // Insert order into database
            await _orderRepository.CreateOrderAsync(order);

            return order;
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



        public async Task<OrderWithDetailsDto> GetOrderWithDetailsByIdAsync(string orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null) return null;

            var detailedItems = new List<OrderItemWithDetailsDto>();

            foreach (var item in order.Items)
            {
                var product = await _productRepository.GetProductByIdAsync(item.ProductId);
                var vendor = await _userRepository.GetUserByIdAsync(item.VendorId); // Assume you have user repository to get vendor details

                if (product != null && vendor != null)
                {
                    detailedItems.Add(new OrderItemWithDetailsDto
                    {
                        Product = new ProductDto
                        {
                            ProductId = product.ProductId,
                            Name = product.Name,
                            Price = product.Price,
                            Description = product.Description,
                            Stock = product.Stock,
                            ImageUrls = product.ImageUrls
                        },
                        Vendor = new VendorDto
                        {
                            VendorId = vendor.Id,
                            VendorName = vendor.FirstName + " " + vendor.LastName,
                            Email = vendor.Email,
                            PhoneNumber = vendor.PhoneNumber,
                        },
                        Quantity = item.Quantity,
                        IsDelivered = item.IsDelivered
                    });
                }
            }

            return new OrderWithDetailsDto
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                Items = detailedItems,
                Status = order.Status,
                Note = order.Note,
                CancellationReason = order.CancellationReason,
                CreatedAt = order.CreatedAt,
                TotalValue = await CalculateTotalValueAsync(order) // Reuse existing logic
            };
        }

        public async Task<IEnumerable<OrderWithDetailsDto>> GetAllOrdersWithDetailsAsync()
        {
            var orders = await _orderRepository.GetAllOrdersAsync();
            var ordersWithDetailsDtos = new List<OrderWithDetailsDto>();

            foreach (var order in orders)
            {
                var detailedItems = new List<OrderItemWithDetailsDto>();

                foreach (var item in order.Items)
                {
                    var product = await _productRepository.GetProductByIdAsync(item.ProductId);
                    var vendor = await _userRepository.GetUserByIdAsync(item.VendorId); // Adjust if necessary

                    if (product != null && vendor != null)
                    {
                        detailedItems.Add(new OrderItemWithDetailsDto
                        {
                            Product = new ProductDto
                            {
                                ProductId = product.ProductId,
                                Name = product.Name,
                                Price = product.Price,
                                Description = product.Description,
                                Stock = product.Stock,
                                ImageUrls = product.ImageUrls
                            },
                            Vendor = new VendorDto
                            {
                                VendorId = vendor.Id,
                                VendorName = vendor.FirstName + " " + vendor.LastName,
                                Email = vendor.Email,
                                PhoneNumber = vendor.PhoneNumber,
                            },
                            Quantity = item.Quantity,
                            IsDelivered = item.IsDelivered
                        });
                    }
                }

                ordersWithDetailsDtos.Add(new OrderWithDetailsDto
                {
                    Id = order.Id,
                    CustomerId = order.CustomerId,
                    Items = detailedItems,
                    Status = order.Status,
                    Note = order.Note,
                    CancellationReason = order.CancellationReason,
                    CreatedAt = order.CreatedAt,
                    TotalValue = await CalculateTotalValueAsync(order) // Reuse existing logic
                });
            }

            return ordersWithDetailsDtos;
        }



        public async Task<OrderWithDetailsDto> GetDetailedOrderByIdAsync(string orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null)
                return null;

            var detailedItems = new List<OrderItemWithDetailsDto>();

            foreach (var item in order.Items)
            {
                var product = await _productRepository.GetProductByIdAsync(item.ProductId);
                var vendor = await _userRepository.GetUserByIdAsync(item.VendorId);

                if (product != null && vendor != null)
                {
                    detailedItems.Add(new OrderItemWithDetailsDto
                    {
                        Product = new ProductDto
                        {
                            ProductId = product.ProductId,
                            Name = product.Name,
                            Price = product.Price,
                            Description = product.Description,
                            Stock = product.Stock,
                            ImageUrls = product.ImageUrls
                        },
                        Vendor = new VendorDto
                        {
                            VendorId = vendor.Id,
                            VendorName = $"{vendor.FirstName} {vendor.LastName}",
                            Email = vendor.Email,
                            PhoneNumber = vendor.PhoneNumber
                        },
                        Quantity = item.Quantity,
                        IsDelivered = item.IsDelivered
                    });
                }
            }

            return new OrderWithDetailsDto
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                Items = detailedItems,
                Status = order.Status,
                CancellationReason = order.CancellationReason,
                Note = order.Note,
                CreatedAt = order.CreatedAt,
                TotalValue = await CalculateTotalValueAsync(order)
            };
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
