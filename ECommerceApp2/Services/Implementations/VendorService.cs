/*
 * File Name: VendorService.cs
 * Author: Thushan Vithana
 * Date: October 6, 2024
 * Description: Implementation of the IVendorService interface to manage vendor operations in the e-commerce application.
 *              This service provides functionality to:
 *              - Add and update vendor ratings based on customer feedback.
 *              - Retrieve vendor information by vendor ID.
 *              - Retrieve a list of all vendors in the system.
 *              - Create new vendor profiles in the application.
 *              - Update existing vendor profiles with new information.
 *              - Delete vendor profiles from the system.
 *              The service utilizes the IVendorRepository interface to interact with the underlying data store 
 *              and ensure efficient vendor management.
 */
using System.Threading.Tasks;
using System.Collections.Generic;
using ECommerceApp2.Models;
using ECommerceApp2.Repositories.Interfaces;
using ECommerceApp2.Services.Interfaces;

namespace ECommerceApp2.Services.Implementations
{
    public class VendorService : IVendorService
    {
        private readonly IVendorRepository _vendorRepository;

        public VendorService(IVendorRepository vendorRepository)
        {
            _vendorRepository = vendorRepository;
        }


        public async Task AddVendorRatingAsync(string vendorId, double rating)
        {
            await UpdateVendorRatingAsync(vendorId, rating);
        }



        public async Task<Vendor> GetVendorByIdAsync(string vendorId)
        {
            return await _vendorRepository.GetVendorByIdAsync(vendorId);
        }

        public async Task<IEnumerable<Vendor>> GetAllVendorsAsync()
        {
            return await _vendorRepository.GetAllVendorsAsync();
        }

        public async Task CreateVendorAsync(Vendor vendor)
        {
            await _vendorRepository.CreateVendorAsync(vendor);
        }

        public async Task UpdateVendorAsync(Vendor vendor)
        {
            await _vendorRepository.UpdateVendorAsync(vendor);
        }

        public async Task DeleteVendorAsync(string vendorId)
        {
            await _vendorRepository.DeleteVendorAsync(vendorId);
        }

        public async Task UpdateVendorRatingAsync(string vendorId, double newRating)
        {
            var vendor = await _vendorRepository.GetVendorByIdAsync(vendorId);
            if (vendor == null)
                throw new KeyNotFoundException("Vendor not found.");

            // Update the average rating
            vendor.TotalRatings += 1;
            vendor.AverageRating = ((vendor.AverageRating * (vendor.TotalRatings - 1)) + newRating) / vendor.TotalRatings;

            await _vendorRepository.UpdateVendorAsync(vendor);
        }
    }
}
