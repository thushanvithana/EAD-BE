/*
 * File Name: ActiveCategoryProductDataDto.cs
 * Author: Thushan Vithana
 * Date: October 6, 2024
 * Description: Data Transfer Object (DTO) representing active category product data.
 *              Contains the category ID, category name, and a list of product details.
 *              This DTO is used for transferring data between the API and the client, 
 *              specifically for fetching active products within a given category.
 */

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
