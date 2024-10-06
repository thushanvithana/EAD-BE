
using ECommerceApp2.Models;
using ECommerceApp2.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerceApp2.Services.Interfaces
{
    public interface IProductCategoryService
    {
        Task<IEnumerable<ProductCategory>> GetAllAsync();
        Task<ProductCategory> GetByIdAsync(string id);
        Task AddAsync(ProductCategory category);
        Task UpdateAsync(ProductCategory category);
        Task DeleteAsync(string id);
        Task ActivateAsync(string id);
        Task DeactivateAsync(string id);

        // Add product to a category
        Task AddProductToCategoryAsync(string categoryId, Product product);
        Task<IEnumerable<ProductCategory>> GetActiveCategoriesAsync();
        Task<IEnumerable<ProductCategory>> GetInactiveCategoriesAsync();



        Task<IEnumerable<CategoryProductCount>> GetProductCountPerCategoryAsync();
        Task<int> GetTotalCategoryCountAsync();



        // New method to get active categories with product details
        Task<IEnumerable<ActiveCategoryProductDataDto>> GetActiveCategoriesWithProductDetailsAsync();
    }
}
