/*
 * File Name: ProductDetailDto.cs
 * Author: Thushan Vithana
 * Date: October 6, 2024
 * Description: Data Transfer Object (DTO) representing detailed product information.
 *              Contains details such as product ID, name, price, and a list of image URLs.
 *              This DTO is used for transferring product detail data between the API and the client.
 */
using System.Collections.Generic;

namespace ECommerceApp2.Models.DTOs
{
    public class ProductDetailDto
    {
        public string ProductId { get; set; }    // ID of the product
        public string Name { get; set; }         // Name of the product
        public decimal Price { get; set; }       // Price of the product
        public List<string> ImageUrls { get; set; } // List of image URLs
    }
}
