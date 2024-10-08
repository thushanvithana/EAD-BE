/*
 * File Name: OrderWithDetailsDto.cs
 * Author: Thushan Vithana
 * Date: October 6, 2024
 * Description: Data Transfer Object (DTO) representing an order with detailed product and vendor information.
 *              Contains details such as order ID, customer ID, order items with product and vendor details, 
 *              status, note, cancellation reason, created date, and total value.
 *              
 *              The OrderItemWithDetailsDto class represents an item within the order, 
 *              including detailed product and vendor information, quantity, and delivery status.
 *              
 *              The ProductDto class represents product details, including product ID, name, price, description, 
 *              stock, and image URLs.
 *              
 *              The VendorDto class represents vendor details, including vendor ID, vendor name, email, and phone number.
 */
using System;
using System.Collections.Generic;

namespace ECommerceApp2.Models
{
    public class OrderWithDetailsDto
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public List<OrderItemWithDetailsDto> Items { get; set; }
        public OrderStatus Status { get; set; }
        public string Note { get; set; }
        public string CancellationReason { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal TotalValue { get; set; }
    }

    public class OrderItemWithDetailsDto
    {
        public ProductDto Product { get; set; }
        public VendorDto Vendor { get; set; }
        public int Quantity { get; set; }
        public bool IsDelivered { get; set; }
    }

    public class ProductDto
    {
        public string ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public List<string> ImageUrls { get; set; }
    }

    public class VendorDto
    {
        public string VendorId { get; set; }
        public string VendorName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
