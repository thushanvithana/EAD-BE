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
