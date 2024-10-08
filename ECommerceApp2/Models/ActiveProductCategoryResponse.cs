/*
 * File Name: ActiveProductCategoryResponse.cs
 * Author: Thushan Vithana
 * Date: October 6, 2024
 * Description: Data Transfer Object (DTO) representing the response for active products in a category.
 *              Contains details such as product ID, product name, price, and image URL.
 *              This DTO is used for transferring active product data between the API and the client.
 */
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace ECommerceApp2.Models
{
    // Models/ActiveProductCategoryResponse.cs
    public class ActiveProductCategoryResponse
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
    }

}
