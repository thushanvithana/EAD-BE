﻿/*
 * File Name: Vendor.cs
 * Author: Thushan Vithana
 * Date: October 6, 2024
 * Description: Defines the Vendor model with properties that represent vendor-related 
 *              data in the application, including fields like Id, Name, ProductIds, 
 *              AverageRating, and TotalRatings.
 */
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace ECommerceApp2.Models
{
    public class Vendor
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }
        public List<string> ProductIds { get; set; } = new List<string>();
        public double AverageRating { get; set; } = 0.0;
        public int TotalRatings { get; set; } = 0;
    }
}
