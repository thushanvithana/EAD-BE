﻿/*
 * File Name: IUserRepository.cs
 * Author: Thushan Vithana
 * Date: October 6, 2024
 * Description: Interface defining the contract for user-related data access methods,
 *              including CRUD operations and any other specific queries related to users.
 */

using ECommerceApp2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerceApp2.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserById(string id);
        Task<User> GetUserByIdAsync(string userId);
        Task<User> GetUserByEmail(string email);
        Task<List<User>> GetAllUsers();
        Task AddUser(User user);
        Task UpdateUser(User user);
        Task DeleteUser(string id);
        Task<User> GetUserByEmailAsync(string email);
        Task<IEnumerable<User>> GetActivatedUsers();
        Task<IEnumerable<User>> GetDeactivatedUsers();

    }
}
