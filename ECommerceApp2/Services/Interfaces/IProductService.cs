/*
 * File Name: IProductService.cs
 * Author: Thushan Vithana
 * Date: October 6, 2024
 * Description: Defines the contract for product-related services in the e-commerce application, 
 *              outlining methods for managing products, including their creation, retrieval, 
 *              updates, and image management.
 */
 using System.Threading.Tasks;
using System.Collections.Generic;
using ECommerceApp2.Models;

namespace ECommerceApp2.Services.Interfaces
{
    public interface IProductService
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


        // New method signatures for managing images
        Task AddImageAsync(string productId, string imageUrl);
        Task RemoveImageAsync(string productId, string imageUrl);


    }
}
