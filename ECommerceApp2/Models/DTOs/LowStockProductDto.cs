// ECommerceApp2.DTOs/LowStockProductDto.cs
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
