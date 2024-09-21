using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace ECommerceApp2.Models
{
    public enum OrderStatus
    {
        Processing,
        ReadyForDelivery,
        PartiallyDelivered,
        Delivered,
        Cancelled
    }

    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string CustomerId { get; set; }
        public List<OrderItem> Items { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Processing;
        public string CancellationReason { get; set; }
        public string Note { get; set; }
    }

    public class OrderItem
    {
        public string ProductId { get; set; }
        public string VendorId { get; set; }
        public int Quantity { get; set; }
        public bool IsDelivered { get; set; } = false;
    }
}
