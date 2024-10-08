/*
 * File Name: CreateOrderRequest.cs
 * Author: Thushan Vithana
 * Date: October 6, 2024
 * Description: Data Transfer Object (DTO) representing the request to create a new order.
 *              Contains the customer's email, a list of order items, and an optional note.
 *              This DTO is used for transferring order creation data between the client and the API.
 */
using System.Collections.Generic;

namespace ECommerceApp2.Models
{
    public class CreateOrderRequest
    {
        public string CustomerEmail { get; set; }
        public List<OrderItemDto> Items { get; set; }
        public string Note { get; set; }
    }
}
