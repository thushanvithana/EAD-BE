/*
 * File Name: IProductRepository.cs
 * Author: Thushan Vithana
 * Date: October 6, 2024
 * Description: Interface for the Product repository, defining operations for managing products 
 *              in the e-commerce application. This interface provides methods to:
 *              - Retrieve a product by its ID
 *              - Retrieve products by vendor ID
 *              - Create a new product
 *              - Update an existing product
 *              - Delete a product by its ID
 *              - Retrieve all products
 *              - Retrieve active products
 *              - Retrieve deactivated products
 *              - Activate or deactivate a product
 *              Implementations of this interface will handle interactions with the database.
 */
using System.Threading.Tasks;
using System.Collections.Generic;
using ECommerceApp2.Models;

namespace ECommerceApp2.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetProductByIdAsync(string productId);

        Task<IEnumerable<Product>> GetProductsByVendorIdAsync(string vendorId);

        Task CreateProductAsync(Product product);

        Task UpdateProductAsync(Product product);

        Task DeleteProductAsync(string productId);

        Task<IEnumerable<Product>> GetAllProductsAsync();

        Task<IEnumerable<Product>> GetActiveProductsAsync();

        Task<IEnumerable<Product>> GetDeactivatedProductsAsync(); // New Method

        Task ActivateProductAsync(string productId);

        Task DeactivateProductAsync(string productId);
    }
}
