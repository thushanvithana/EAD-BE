using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ECommerceApp2.Models
{
    public class Rating
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string VendorId { get; set; }
        public string CustomerId { get; set; }
        public int Stars { get; set; } // 1 to 5
        public string Comment { get; set; }
    }
}
