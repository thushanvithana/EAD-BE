using ECommerceApp2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerceApp2.Repositories.Interfaces
{
    public interface IProductCategoryRepository
    {
        Task<IEnumerable<ProductCategory>> GetAllAsync();
        Task<ProductCategory> GetByIdAsync(string id);
        Task AddAsync(ProductCategory category);
        Task UpdateAsync(ProductCategory category);
        Task DeleteAsync(string id);
        Task ActivateAsync(string id);
        Task DeactivateAsync(string id);

        // Add product to a specific category
        Task AddProductToCategoryAsync(string categoryId, Product product);
    }
}
