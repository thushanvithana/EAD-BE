/*
 * File Name: IProductCategoryRepository.cs
 * Author: Thushan Vithana
 * Date: October 6, 2024
 * Description: Interface for the Product Category repository, defining operations for managing product 
 *              categories in the e-commerce application. This interface provides methods to:
 *              - Retrieve all product categories
 *              - Retrieve a product category by its ID
 *              - Add a new product category
 *              - Update an existing product category
 *              - Delete a product category by its ID
 *              - Activate or deactivate a product category
 *              - Add a product to a specific category
 *              - Retrieve active and inactive product categories
 *              - Get the product count per category
 *              - Get the total count of categories
 *              Implementations of this interface will handle interactions with the database.
 */
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

        Task<IEnumerable<ProductCategory>> GetActiveCategoriesAsync();
        Task<IEnumerable<ProductCategory>> GetInactiveCategoriesAsync();


        Task<IEnumerable<ProductCategory>> GetActiveCategoriesAsyncq();

        Task<IEnumerable<CategoryProductCount>> GetProductCountPerCategoryAsync();
        Task<int> GetTotalCategoryCountAsync();
    }
}
