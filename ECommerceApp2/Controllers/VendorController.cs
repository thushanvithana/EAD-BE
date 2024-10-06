/*
 * File Name: VendorController.cs
 * Author: Thushan Vithana
 * Date: October 6, 2024
 * Description: ASP.NET Controller responsible for handling HTTP requests related to vendors,
 *              including operations such as retrieving vendor details by ID, listing all vendors, 
 *              creating new vendors, updating existing vendors, and deleting vendors. It interfaces 
 *              with the IVendorService to perform business logic and return appropriate HTTP responses.
 */

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerceApp2.Models;
using ECommerceApp2.Services.Interfaces;

namespace ECommerceApp2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        private readonly IVendorService _vendorService;

        public VendorController(IVendorService vendorService)
        {
            _vendorService = vendorService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Vendor>> GetVendor(string id)
        {
            var vendor = await _vendorService.GetVendorByIdAsync(id);
            if (vendor == null)
                return NotFound();
            return vendor;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vendor>>> GetAllVendors()
        {
            return Ok(await _vendorService.GetAllVendorsAsync());
        }

        [HttpPost]
        public async Task<ActionResult<Vendor>> CreateVendor([FromBody] Vendor vendor)
        {
            await _vendorService.CreateVendorAsync(vendor);
            return CreatedAtAction(nameof(GetVendor), new { id = vendor.Id }, vendor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVendor(string id, [FromBody] Vendor vendor)
        {
            if (id != vendor.Id)
                return BadRequest();

            await _vendorService.UpdateVendorAsync(vendor);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVendor(string id)
        {
            await _vendorService.DeleteVendorAsync(id);
            return NoContent();
        }
    }
}
