
/*
 * File Name: ProductController.cs
 * Author: Thushan Vithana
 * Date: October 6, 2024
 * Description: ASP.NET Controller responsible for handling HTTP requests related to products, 
 *              including operations such as retrieving products by ID and vendor, creating, updating,
 *              deleting products, activating or deactivating products, and managing product images. 
 *              It interfaces with the IProductService to perform business logic and return 
 *              appropriate HTTP responses.
 */
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using ECommerceApp2.Models;
using ECommerceApp2.Services.Interfaces;

namespace ECommerceApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        // Dependency Injection
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET api/product/{productId}
        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProduct(string productId)
        {
            var product = await _productService.GetProductByIdAsync(productId);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        // GET api/product/vendor/{vendorId}
        [HttpGet("vendor/{vendorId}")]
        public async Task<IActionResult> GetProductsByVendor(string vendorId)
        {
            var products = await _productService.GetProductsByVendorIdAsync(vendorId);
            return Ok(products);
        }

        // POST api/product
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            await _productService.CreateProductAsync(product);
            return CreatedAtAction(nameof(GetProduct), new { productId = product.ProductId }, product);
        }

        // PUT api/product/{productId}
        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateProduct(string productId, [FromBody] Product product)
        {
            if (productId != product.ProductId)
                return BadRequest("Product ID mismatch.");

            await _productService.UpdateProductAsync(product);
            return NoContent();
        }

        // DELETE api/product/{productId}
        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProduct(string productId)
        {
            await _productService.DeleteProductAsync(productId);
            return NoContent();
        }

        // PUT api/product/activate/{productId}
        [HttpPut("activate/{productId}")]
        public async Task<IActionResult> ActivateProduct(string productId)
        {
            await _productService.ActivateProductAsync(productId);
            return NoContent();
        }

        // PUT api/product/deactivate/{productId}
        [HttpPut("deactivate/{productId}")]
        public async Task<IActionResult> DeactivateProduct(string productId)
        {
            await _productService.DeactivateProductAsync(productId);
            return NoContent();
        }

        // GET api/product/active
        [HttpGet("active")]
        public async Task<IActionResult> GetActiveProducts()
        {
            var products = await _productService.GetActiveProductsAsync();
            return Ok(products);
        }



        // POST api/product/{productId}/images
        [HttpPut("{productId}/images")]
        public async Task<IActionResult> AddImage(string productId, [FromBody] string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl))
                return BadRequest("Image URL cannot be null or empty.");

            try
            {
                await _productService.AddImageAsync(productId, imageUrl);
                return Ok("Image updated successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging framework)
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET api/product/all
        [HttpGet("all")]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var products = await _productService.GetAllProductsAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging framework)
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET api/product/deactivated
        [HttpGet("deactivated")]
        public async Task<IActionResult> GetDeactivatedProducts()
        {
            try
            {
                var products = await _productService.GetDeactivatedProductsAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging framework)
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE api/product/{productId}/images
        [HttpDelete("{productId}/images")]
        public async Task<IActionResult> RemoveImage(string productId, [FromBody] string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl))
                return BadRequest("Image URL cannot be null or empty.");

            try
            {
                await _productService.RemoveImageAsync(productId, imageUrl);
                return Ok("Image removed successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging framework)
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
