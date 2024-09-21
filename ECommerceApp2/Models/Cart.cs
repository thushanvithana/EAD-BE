﻿
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace ECommerceApp2.Models
{
    public class Cart
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string UserId { get; set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();
    }
}
