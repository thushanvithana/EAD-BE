using ECommerceApp2.Models;
using ECommerceApp2.Models.DTOs;
using ECommerceApp2.Repositories.Interfaces;
using ECommerceApp2.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerceApp2.Services.Implementations
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IProductService _productService;
        public ProductCategoryService(IProductCategoryRepository productCategoryRepository, IProductService productService)
        {
            _productCategoryRepository = productCategoryRepository;
            _productService = productService;
        }

        public async Task<IEnumerable<ProductCategory>> GetAllAsync()
        {
            return await _productCategoryRepository.GetAllAsync();
        }

        public async Task<ProductCategory> GetByIdAsync(string id)
        {
            return await _productCategoryRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(ProductCategory category)
        {
            await _productCategoryRepository.AddAsync(category);
        }

        public async Task UpdateAsync(ProductCategory category)
        {
            await _productCategoryRepository.UpdateAsync(category);
        }

        public async Task DeleteAsync(string id)
        {
            await _productCategoryRepository.DeleteAsync(id);
        }

        public async Task ActivateAsync(string id)
        {
            // Activate the category
            await _productCategoryRepository.ActivateAsync(id);

            // Fetch the category to get associated products
            var category = await _productCategoryRepository.GetByIdAsync(id);
            if (category != null && category.Products != null)
            {
                foreach (var product in category.Products)
                {
                    await _productService.ActivateProductAsync(product.ProductId);
                }
            }
        }

        public async Task DeactivateAsync(string id)
        {
            // Deactivate the category
            await _productCategoryRepository.DeactivateAsync(id);

            // Fetch the category to get associated products
            var category = await _productCategoryRepository.GetByIdAsync(id);
            if (category != null && category.Products != null)
            {
                foreach (var product in category.Products)
                {
                    await _productService.DeactivateProductAsync(product.ProductId);
                }
            }
        }


        public async Task AddProductToCategoryAsync(string categoryId, Product product)
        {
            // Step 1: Create the Product
            await _productService.CreateProductAsync(product);

            // Step 2: Add the Product to the Category's Products list
            await _productCategoryRepository.AddProductToCategoryAsync(categoryId, product);
        }
        public async Task<IEnumerable<ProductCategory>> GetActiveCategoriesAsync()
        {
            return await _productCategoryRepository.GetActiveCategoriesAsync();
        }

        public async Task<IEnumerable<ProductCategory>> GetInactiveCategoriesAsync()
        {
            return await _productCategoryRepository.GetInactiveCategoriesAsync();
        }



        public async Task<IEnumerable<CategoryProductCount>> GetProductCountPerCategoryAsync()
        {
            return await _productCategoryRepository.GetProductCountPerCategoryAsync();
        }

        public async Task<int> GetTotalCategoryCountAsync()
        {
            return await _productCategoryRepository.GetTotalCategoryCountAsync();
        }



        // New method implementation to get active categories with product details
        public async Task<IEnumerable<ActiveCategoryProductDataDto>> GetActiveCategoriesWithProductDetailsAsync()
        {
            var activeCategories = await _productCategoryRepository.GetActiveCategoriesAsync();

            // Map to DTO
            var result = activeCategories.Select(category => new ActiveCategoryProductDataDto
            {
                CategoryId = category.Id,
                CategoryName = category.Name,
                Products = category.Products.Select(product => new ProductDetailDto
                {
                    ProductId = product.ProductId,
                    Name = product.Name,
                    Price = product.Price,
                    ImageUrls = product.ImageUrls
                }).ToList()
            });

            return result;
        }

    }
}
