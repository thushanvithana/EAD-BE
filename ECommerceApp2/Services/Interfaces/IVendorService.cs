using System.Threading.Tasks;
using System.Collections.Generic;
using ECommerceApp2.Models;

namespace ECommerceApp2.Services.Interfaces
{
    public interface IVendorService
    {
        Task<Vendor> GetVendorByIdAsync(string vendorId);
        Task<IEnumerable<Vendor>> GetAllVendorsAsync();
        Task CreateVendorAsync(Vendor vendor);
        Task UpdateVendorAsync(Vendor vendor);
        Task DeleteVendorAsync(string vendorId);
        Task UpdateVendorRatingAsync(string vendorId, double newRating);
    }
}
