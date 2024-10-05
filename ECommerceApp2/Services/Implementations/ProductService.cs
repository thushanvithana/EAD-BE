using System;
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
            try
            {
                return await _productRepository.GetProductByIdAsync(productId);
            }
            catch (Exception ex)
            {
                // Log error message here (e.g., using a logging library)
                Console.WriteLine($"Error retrieving product by ID: {productId}, Exception: {ex.Message}");
                throw; // Rethrow the exception after logging
            }
        }

        public async Task<IEnumerable<Product>> GetProductsByVendorIdAsync(string vendorId)
        {
            try
            {
                return await _productRepository.GetProductsByVendorIdAsync(vendorId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving products for vendor ID: {vendorId}, Exception: {ex.Message}");
                throw;
            }
        }

        public async Task CreateProductAsync(Product product)
        {
            try
            {
                product.ProductId = Guid.NewGuid().ToString(); // Set ProductId here if not already set
                await _productRepository.CreateProductAsync(product);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating product: {product.Name}, Exception: {ex.Message}");
                throw;
            }
        }


        public async Task UpdateProductAsync(Product product)
        {
            try
            {
                await _productRepository.UpdateProductAsync(product);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating product ID: {product.ProductId}, Exception: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteProductAsync(string productId)
        {
            try
            {
                await _productRepository.DeleteProductAsync(productId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting product ID: {productId}, Exception: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            try
            {
                return await _productRepository.GetAllProductsAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving all products, Exception: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<Product>> GetActiveProductsAsync()
        {
            try
            {
                return await _productRepository.GetActiveProductsAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving active products, Exception: {ex.Message}");
                throw;
            }
        }



        public async Task<IEnumerable<Product>> GetDeactivatedProductsAsync() // Implementation
        {
            try
            {
                return await _productRepository.GetDeactivatedProductsAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving deactivated products, Exception: {ex.Message}");
                throw;
            }
        }


        public async Task ActivateProductAsync(string productId)
        {
            try
            {
                await _productRepository.ActivateProductAsync(productId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error activating product ID: {productId}, Exception: {ex.Message}");
                throw;
            }
        }

        public async Task DeactivateProductAsync(string productId)
        {
            try
            {
                await _productRepository.DeactivateProductAsync(productId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deactivating product ID: {productId}, Exception: {ex.Message}");
                throw;
            }
        }





        // New method to add an image URL to a product
        public async Task AddImageAsync(string productId, string imageUrl)
        {
            try
            {
                var product = await _productRepository.GetProductByIdAsync(productId);
                if (product == null)
                    throw new Exception($"Product with ID {productId} not found.");

                product.ImageUrls.Add(imageUrl);
                await _productRepository.UpdateProductAsync(product);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding image to product ID: {productId}, Exception: {ex.Message}");
                throw;
            }
        }



        // New method to remove an image URL from a product
        public async Task RemoveImageAsync(string productId, string imageUrl)
        {
            try
            {
                var product = await _productRepository.GetProductByIdAsync(productId);
                if (product == null)
                    throw new Exception($"Product with ID {productId} not found.");

                product.ImageUrls.Remove(imageUrl);
                await _productRepository.UpdateProductAsync(product);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing image from product ID: {productId}, Exception: {ex.Message}");
                throw;
            }
        }
    }
}
