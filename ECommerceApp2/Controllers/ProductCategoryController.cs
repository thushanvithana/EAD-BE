
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

        // New endpoint to add product to category
        [HttpPost("{categoryId}/products")]
        public async Task<IActionResult> AddProductToCategoryAsync(string categoryId, Product product)
        {
            await _productCategoryService.AddProductToCategoryAsync(categoryId, product);
            return Ok();
        }
    }
}
