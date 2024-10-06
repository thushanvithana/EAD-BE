/*
 * File Name: ActiveProductCategoryDto.cs
 * Author: Thushan Vithana
 * Date: October 6, 2024
 * Description: Data Transfer Object (DTO) representing an active product within a category.
 *              Contains properties for the product ID, product name, price, and image URL.
 *              This DTO is used for transferring product data between the API and the client,
 *              specifically for displaying active products in a given category.
 */

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
