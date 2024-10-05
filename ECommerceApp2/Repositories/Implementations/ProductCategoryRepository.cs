using ECommerceApp2.Models;
using ECommerceApp2.Repositories.Interfaces;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerceApp2.Repositories.Implementations
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly IMongoCollection<ProductCategory> _productCategories;

        public ProductCategoryRepository(IMongoDatabase database)
        {
            _productCategories = database.GetCollection<ProductCategory>("ProductCategories");
        }

        public async Task<IEnumerable<ProductCategory>> GetAllAsync()
        {
            return await _productCategories.Find(category => true).ToListAsync();
        }

        public async Task<ProductCategory> GetByIdAsync(string id)
        {
            return await _productCategories.Find(category => category.Id == id).FirstOrDefaultAsync();
        }

        public async Task AddAsync(ProductCategory category)
        {
            await _productCategories.InsertOneAsync(category);
        }

        public async Task UpdateAsync(ProductCategory category)
        {
            await _productCategories.ReplaceOneAsync(c => c.Id == category.Id, category);
        }

        public async Task DeleteAsync(string id)
        {
            await _productCategories.DeleteOneAsync(c => c.Id == id);
        }

        public async Task ActivateAsync(string id)
        {
            var update = Builders<ProductCategory>.Update.Set(c => c.IsActive, true);
            await _productCategories.UpdateOneAsync(c => c.Id == id, update);
        }

        public async Task DeactivateAsync(string id)
        {
            var update = Builders<ProductCategory>.Update.Set(c => c.IsActive, false);
            await _productCategories.UpdateOneAsync(c => c.Id == id, update);
        }

        public async Task AddProductToCategoryAsync(string categoryId, Product product)
        {
            var update = Builders<ProductCategory>.Update.AddToSet(c => c.Products, product);
            await _productCategories.UpdateOneAsync(c => c.Id == categoryId, update);
        }
    }
}
