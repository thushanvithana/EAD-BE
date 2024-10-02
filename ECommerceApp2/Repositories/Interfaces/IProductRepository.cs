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
