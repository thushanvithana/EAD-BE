/*
 * File Name: OrderRepository.cs
 * Author: Thushan Vithana
 * Date: October 6, 2024
 * Description: Implementation of the IOrderRepository interface for managing order records in the MongoDB database.
 *              This repository provides methods to:
 *              - Retrieve an order by its ID
 *              - Retrieve all orders placed by a specific customer
 *              - Retrieve all orders in the system
 *              - Create a new order record
 *              - Update an existing order record
 *              - Delete an order by its ID
 *              Uses MongoDB.Driver to interact with the "Orders" collection in the specified database.
 */

using MongoDB.Driver;
using System.Threading.Tasks;
using System.Collections.Generic;
using ECommerceApp2.Models;
using ECommerceApp2.Repositories.Interfaces;

namespace ECommerceApp2.Repositories.Implementations
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IMongoCollection<Order> _orders;

        public OrderRepository(IMongoDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _orders = database.GetCollection<Order>("Orders");
        }

        public async Task<Order> GetOrderByIdAsync(string orderId)
        {
            return await _orders.Find(o => o.Id == orderId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(string customerId)
        {
            return await _orders.Find(o => o.CustomerId == customerId).ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _orders.Find(_ => true).ToListAsync();
        }

        public async Task CreateOrderAsync(Order order)
        {
            await _orders.InsertOneAsync(order);
        }

        public async Task UpdateOrderAsync(Order order)
        {
            await _orders.ReplaceOneAsync(o => o.Id == order.Id, order);
        }

        public async Task DeleteOrderAsync(string orderId)
        {
            await _orders.DeleteOneAsync(o => o.Id == orderId);
        }
    }
}
