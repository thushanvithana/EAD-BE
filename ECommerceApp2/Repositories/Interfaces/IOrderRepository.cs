/*
 * File Name: IOrderRepository.cs
 * Author: Thushan Vithana
 * Date: October 6, 2024
 * Description: Interface for the Order repository, defining the operations for managing customer orders
 *              in the e-commerce application.
 *              This interface provides methods to:
 *              - Retrieve an order by its ID
 *              - Retrieve all orders associated with a specific customer ID
 *              - Retrieve all orders in the system
 *              - Create a new order
 *              - Update an existing order
 *              - Delete an order by its ID
 *              Implementations of this interface will handle the interaction with the database.
 */
using System.Threading.Tasks;
using System.Collections.Generic;
using ECommerceApp2.Models;

namespace ECommerceApp2.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> GetOrderByIdAsync(string orderId);
        Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(string customerId);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task CreateOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(string orderId);
    }
}
