using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; // Ensure this using directive is present

namespace ECommerceApp2.Models
{
    public enum OrderStatus
    {
        Processing, //0
        ReadyForDelivery, //1
        PartiallyDelivered,//2
        Delivered,//3
        Cancelled//4
    }

    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } // Make Id nullable

        [Required]
        public string CustomerId { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Order must contain at least one item.")]
        public List<OrderItem> Items { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Processing;
        public string CancellationReason { get; set; }
        public string Note { get; set; }

        // New properties
        public decimal TotalValue { get; set; }
        public DateTime CreatedAt { get; set; }

        public Order()
        {
            // Generate a new ObjectId for the order
            Id = ObjectId.GenerateNewId().ToString();
            CreatedAt = DateTime.UtcNow; // Auto-generate creation time
            TotalValue = 0m; // Initialize to zero
        }
    }

    public class OrderItem
    {
        [Required]
        public string ProductId { get; set; }

        [Required]
        public string VendorId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        public bool IsDelivered { get; set; } = false;

        [Range(0.01, double.MaxValue, ErrorMessage = "UnitPrice must be greater than zero.")]
        public decimal UnitPrice { get; set; }
    }
}
