
/*
 * File Name: ProductCategoryController.cs
 * Author: Thushan Vithana
 * Date: October 6, 2024
 * Description: ASP.NET Controller responsible for handling HTTP requests related to product categories,
 *              including operations such as retrieving all categories, adding, updating, and deleting 
 *              categories, activating or deactivating categories, and managing products within categories. 
 *              It interfaces with the IProductCategoryService to perform business logic and return 
 *              appropriate HTTP responses.
 */

using ECommerceApp2.Models;
using ECommerceApp2.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerceApp2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IProductCategoryService _productCategoryService;

        public ProductCategoryController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductCategory>> GetAllAsync()
        {
            return await _productCategoryService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ProductCategory> GetByIdAsync(string id)
        {
            return await _productCategoryService.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(ProductCategory category)
        {
            await _productCategoryService.AddAsync(category);
            return Ok();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(string id, ProductCategory category)
        {
            if (id != category.Id) return BadRequest();
            await _productCategoryService.UpdateAsync(category);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            await _productCategoryService.DeleteAsync(id);
            return Ok();
        }

        [HttpPatch("{id}/activate")]
        public async Task<IActionResult> ActivateAsync(string id)
        {
            await _productCategoryService.ActivateAsync(id);
            return Ok();
        }

        [HttpPatch("{id}/deactivate")]
        public async Task<IActionResult> DeactivateAsync(string id)
        {
            await _productCategoryService.DeactivateAsync(id);
            return Ok();
        }

        [HttpPost("{categoryId}/products")]
        public async Task<IActionResult> AddProductToCategoryAsync(string categoryId, [FromBody] Product product)
        {
            if (product == null)
                return BadRequest("Product data is null.");

            try
            {
                await _productCategoryService.AddProductToCategoryAsync(categoryId, product);
                return Ok(new { Message = "Product added to category successfully.", Product = product });
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging framework)
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


           // New endpoint to get all active product categories
        [HttpGet("active")]
        public async Task<IActionResult> GetActiveCategoriesAsync()
        {
            try
            {
                var activeCategories = await _productCategoryService.GetActiveCategoriesAsync();
                return Ok(activeCategories);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // New endpoint to get all inactive product categories
        [HttpGet("inactive")]
        public async Task<IActionResult> GetInactiveCategoriesAsync()
        {
            try
            {
                var inactiveCategories = await _productCategoryService.GetInactiveCategoriesAsync();
                return Ok(inactiveCategories);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        // New endpoint to get count of products in each category
        [HttpGet("product-count")]
        public async Task<IActionResult> GetProductCountPerCategoryAsync()
        {
            try
            {
                var productCounts = await _productCategoryService.GetProductCountPerCategoryAsync();
                return Ok(productCounts);
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging framework)
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // New endpoint to get total count of categories created
        [HttpGet("category-count")]
        public async Task<IActionResult> GetTotalCategoryCountAsync()
        {
            try
            {
                var totalCount = await _productCategoryService.GetTotalCategoryCountAsync();
                var response = new CategoryCountResponse
                {
                    CategoryCount = totalCount
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging framework)
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        // New endpoint to get active category product data with specific product details
        [HttpGet("active-products")]
        public async Task<IActionResult> GetActiveCategoriesWithProductDetailsAsync()
        {
            try
            {
                var data = await _productCategoryService.GetActiveCategoriesWithProductDetailsAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging framework)
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
