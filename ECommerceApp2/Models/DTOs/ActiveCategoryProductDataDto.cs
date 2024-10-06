using System.Collections.Generic;

namespace ECommerceApp2.Models.DTOs
{
    public class ActiveCategoryProductDataDto
    {
        public string CategoryId { get; set; }         // ID of the category
        public string CategoryName { get; set; }       // Name of the category
        public List<ProductDetailDto> Products { get; set; } // List of product details
    }
}
