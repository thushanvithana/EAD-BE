// ECommerceApp2.DTOs/LowStockProductDto.cs
/*
 * File Name: LowStockProductDto.cs
 * Author: Thushan Vithana
 * Date: October 6, 2024
 * Description: Data Transfer Object (DTO) representing a product with low stock levels.
 *              Contains details such as product ID, name, category, vendor ID, 
 *              price, description, available stock, and the low stock threshold.
 *              This DTO is used for transferring low stock product data between the API and the client.
 */
namespace ECommerceApp2.DTOs
{
    public class LowStockProductDto
    {
        public string ProductId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string VendorId { get; set; }
        public bool IsActive { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int AvailableStock { get; set; }
        public int LowStockThreshold { get; set; }
    }
}
