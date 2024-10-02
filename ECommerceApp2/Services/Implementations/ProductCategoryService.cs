
using ECommerceApp2.Models;
using ECommerceApp2.Repositories.Interfaces;
using ECommerceApp2.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerceApp2.Services.Implementations
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductCategoryService(IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
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
            await _productCategoryRepository.ActivateAsync(id);
        }

        public async Task DeactivateAsync(string id)
        {
            await _productCategoryRepository.DeactivateAsync(id);
        }

        public async Task AddProductToCategoryAsync(string categoryId, Product product)
        {
            await _productCategoryRepository.AddProductToCategoryAsync(categoryId, product);
        }
    }
}
