/*
 * File Name: ProductRepository.cs
 * Author: Thushan Vithana
 * Date: October 6, 2024
 * Description: Implementation of the IProductRepository interface for managing products in the MongoDB database.
 *              This repository provides methods to:
 *              - Retrieve a product by its ID
 *              - Retrieve products by vendor ID
 *              - Create a new product
 *              - Update an existing product
 *              - Delete a product by its ID
 *              - Retrieve all products
 *              - Retrieve all active products
 *              - Retrieve all deactivated products
 *              - Activate a product by its ID
 *              - Deactivate a product by its ID
 *              Utilizes MongoDB.Driver for operations on the "Products" collection in the specified database.
 */

using ECommerceApp2.Repositories.Interfaces;
using ECommerceApp2.Models;
using MongoDB.Driver;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ECommerceApp2.Repositories.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _products;

        public ProductRepository(IMongoDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _products = database.GetCollection<Product>("Products");
        }

        public async Task<Product> GetProductByIdAsync(string productId)
        {
            return await _products.Find(p => p.ProductId == productId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByVendorIdAsync(string vendorId)
        {
            return await _products.Find(p => p.VendorId == vendorId).ToListAsync();
        }

        public async Task CreateProductAsync(Product product)
        {
            await _products.InsertOneAsync(product);
        }

        public async Task UpdateProductAsync(Product product)
        {
            await _products.ReplaceOneAsync(p => p.ProductId == product.ProductId, product);
        }

        public async Task DeleteProductAsync(string productId)
        {
            await _products.DeleteOneAsync(p => p.ProductId == productId);
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _products.Find(_ => true).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetActiveProductsAsync()
        {
            return await _products.Find(p => p.IsActive).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetDeactivatedProductsAsync() // Implementation
        {
            return await _products.Find(p => !p.IsActive).ToListAsync();
        }


        public async Task ActivateProductAsync(string productId)
        {
            var update = Builders<Product>.Update.Set(p => p.IsActive, true);
            await _products.UpdateOneAsync(p => p.ProductId == productId, update);
        }

        public async Task DeactivateProductAsync(string productId)
        {
            var update = Builders<Product>.Update.Set(p => p.IsActive, false);
            await _products.UpdateOneAsync(p => p.ProductId == productId, update);
        }




    }
}
