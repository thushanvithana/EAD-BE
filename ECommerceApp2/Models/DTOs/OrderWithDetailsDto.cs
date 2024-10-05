using System;
using System.Collections.Generic;

namespace ECommerceApp2.Models
{
    public class OrderWithDetailsDto
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public List<OrderItemWithDetailsDto> Items { get; set; }
        public OrderStatus Status { get; set; }
        public string Note { get; set; }
        public string CancellationReason { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal TotalValue { get; set; }
    }

    public class OrderItemWithDetailsDto
    {
        public ProductDto Product { get; set; }
        public VendorDto Vendor { get; set; }
        public int Quantity { get; set; }
        public bool IsDelivered { get; set; }
    }

    public class ProductDto
    {
        public string ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public List<string> ImageUrls { get; set; }
    }

    public class VendorDto
    {
        public string VendorId { get; set; }
        public string VendorName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
