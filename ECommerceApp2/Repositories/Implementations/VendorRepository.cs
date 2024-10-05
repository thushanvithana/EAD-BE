using MongoDB.Driver;
using System.Threading.Tasks;
using System.Collections.Generic;
using ECommerceApp2.Models;
using ECommerceApp2.Repositories.Interfaces;

namespace ECommerceApp2.Repositories.Implementations
{
    public class VendorRepository : IVendorRepository
    {
        private readonly IMongoCollection<Vendor> _vendors;

        public VendorRepository(IMongoDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _vendors = database.GetCollection<Vendor>("Vendors");
        }

        public async Task<Vendor> GetVendorByIdAsync(string vendorId)
        {
            return await _vendors.Find(v => v.Id == vendorId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Vendor>> GetAllVendorsAsync()
        {
            return await _vendors.Find(_ => true).ToListAsync();
        }

        public async Task CreateVendorAsync(Vendor vendor)
        {
            await _vendors.InsertOneAsync(vendor);
        }

        public async Task UpdateVendorAsync(Vendor vendor)
        {
            await _vendors.ReplaceOneAsync(v => v.Id == vendor.Id, vendor);
        }



        public async Task DeleteVendorAsync(string vendorId)
        {
            await _vendors.DeleteOneAsync(v => v.Id == vendorId);
        }
    }
}
