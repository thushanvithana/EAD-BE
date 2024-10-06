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
        private readonly IMongoClient _mongoClient;
        public ProductCategoryRepository(IMongoDatabase database, IMongoClient mongoClient)
        {
            _productCategories = database.GetCollection<ProductCategory>("ProductCategories");
            _mongoClient = mongoClient;
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
            using (var session = await _mongoClient.StartSessionAsync())
            {
                session.StartTransaction();

                try
                {
                    var update = Builders<ProductCategory>.Update.AddToSet(c => c.Products, product);
                    await _productCategories.UpdateOneAsync(session, c => c.Id == categoryId, update);

                    // Commit the transaction
                    await session.CommitTransactionAsync();
                }
                catch
                {
                    // Abort the transaction on error
                    await session.AbortTransactionAsync();
                    throw;
                }
            }
        }


        public async Task<IEnumerable<ProductCategory>> GetActiveCategoriesAsync()
        {
            var filter = Builders<ProductCategory>.Filter.Eq(c => c.IsActive, true);
            return await _productCategories.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<ProductCategory>> GetInactiveCategoriesAsync()
        {
            var filter = Builders<ProductCategory>.Filter.Eq(c => c.IsActive, false);
            return await _productCategories.Find(filter).ToListAsync();
        }


        public async Task<IEnumerable<CategoryProductCount>> GetProductCountPerCategoryAsync()
        {
            var aggregate = _productCategories.Aggregate()
                .Project(c => new CategoryProductCount
                {
                    CategoryId = c.Id,
                    CategoryName = c.Name,
                    ProductCount = c.Products.Count
                });

            return await aggregate.ToListAsync();
        }

        public async Task<int> GetTotalCategoryCountAsync()
        {
            return (int)await _productCategories.CountDocumentsAsync(FilterDefinition<ProductCategory>.Empty);
        }



        // New method implementation to get active categories
        public async Task<IEnumerable<ProductCategory>> GetActiveCategoriesAsyncq()
        {
            var filter = Builders<ProductCategory>.Filter.Eq(c => c.IsActive, true);
            return await _productCategories.Find(filter).ToListAsync();
        }


    }
}
