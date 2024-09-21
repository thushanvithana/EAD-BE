using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ECommerceApp2.Models
{
    public class Inventory
    {
        [BsonId]
        public string ProductId { get; set; }
        public int AvailableStock { get; set; }
        public int LowStockThreshold { get; set; } = 10; // Default threshold
    }
}
