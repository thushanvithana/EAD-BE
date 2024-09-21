using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ECommerceApp2.Models
{
    public class CartItem
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
