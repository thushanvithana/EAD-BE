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
