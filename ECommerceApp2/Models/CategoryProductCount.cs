using MongoDB.Bson.Serialization.Attributes;

namespace ECommerceApp2.Models
{
    public class CategoryProductCount
    {
        [BsonId]
        public string CategoryId { get; set; }

        public string CategoryName { get; set; }

        public int ProductCount { get; set; }
    }
}
