using System.Threading.Tasks;
using System.Collections.Generic;
using ECommerceApp2.Models;

namespace ECommerceApp2.Repositories.Interfaces
{
    public interface IVendorRepository
    {
        Task<Vendor> GetVendorByIdAsync(string vendorId);
        Task<IEnumerable<Vendor>> GetAllVendorsAsync();
        Task CreateVendorAsync(Vendor vendor);
        Task UpdateVendorAsync(Vendor vendor);
        Task DeleteVendorAsync(string vendorId);
    }
}
