
using System.Threading.Tasks;
using System.Collections.Generic;
using ECommerceApp2.Models;
using ECommerceApp2.Repositories.Interfaces;
using ECommerceApp2.Services.Interfaces;

namespace ECommerceApp2.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        // Additional dependencies like logging can be injected here

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> GetProductByIdAsync(string productId)
        {
            return await _productRepository.GetProductByIdAsync(productId);
        }

        public async Task<IEnumerable<Product>> GetProductsByVendorIdAsync(string vendorId)
        {
            return await _productRepository.GetProductsByVendorIdAsync(vendorId);
        }

        public async Task CreateProductAsync(Product product)
        {
            // Business logic can be added here (e.g., validation)
            await _productRepository.CreateProductAsync(product);
        }

        public async Task UpdateProductAsync(Product product)
        {
            // Business logic can be added here
            await _productRepository.UpdateProductAsync(product);
        }

        public async Task DeleteProductAsync(string productId)
        {
            // Business logic can be added here (e.g., check if product is part of any pending orders)
            await _productRepository.DeleteProductAsync(productId);
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllProductsAsync();
        }

        public async Task<IEnumerable<Product>> GetActiveProductsAsync()
        {
            return await _productRepository.GetActiveProductsAsync();
        }

        public async Task ActivateProductAsync(string productId)
        {
            await _productRepository.ActivateProductAsync(productId);
        }

        public async Task DeactivateProductAsync(string productId)
        {
            await _productRepository.DeactivateProductAsync(productId);
        }
    }
}
