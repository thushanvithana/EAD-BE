using System;
using System.Collections.Generic;

namespace ECommerceApp2.Models
{
    public class ActiveProductCategoryDto
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
    }

}
