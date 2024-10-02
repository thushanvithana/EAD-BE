using System;
using System.Collections.Generic;

namespace ECommerceApp2.Models
{
    public class OrderDto
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public List<OrderItemDto> Items { get; set; }
        public OrderStatus Status { get; set; }
        public string CancellationReason { get; set; }
        public string Note { get; set; }
        public DateTime CreatedAt { get; set; }

        // New property
        public decimal TotalValue { get; set; }
    }

    public class OrderItemDto
    {
        public string ProductId { get; set; }
        public string VendorId { get; set; }
        public int Quantity { get; set; }
        public bool IsDelivered { get; set; }
    }
}
