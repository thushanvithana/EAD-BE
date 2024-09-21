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
