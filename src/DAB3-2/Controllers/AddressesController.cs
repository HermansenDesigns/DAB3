using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAB32.Models;

namespace DAB3_2.Controllers
{
    [Produces("application/json")]
    [Route("api/Addresses")]
    public class AddressesController : Controller
    {
        private readonly F184DABH2Gr24Context _context;

        public AddressesController(F184DABH2Gr24Context context)
        {
            _context = context;
        }

        // GET: api/Addresses
        [HttpGet]
        public IEnumerable<Addresses> GetAddresses()
        {
            return _context.Addresses;
        }

        // GET: api/Addresses/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAddresses([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var addresses = await _context.Addresses.SingleOrDefaultAsync(m => m.Id == id);

            if (addresses == null)
            {
                return NotFound();
            }

            return Ok(addresses);
        }

        // PUT: api/Addresses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddresses([FromRoute] int id, [FromBody] Addresses addresses)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != addresses.Id)
            {
                return BadRequest();
            }

            _context.Entry(addresses).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Addresses
        [HttpPost]
        public async Task<IActionResult> PostAddresses([FromBody] Addresses addresses)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Addresses.Add(addresses);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAddresses", new { id = addresses.Id }, addresses);
        }

        // DELETE: api/Addresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddresses([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var addresses = await _context.Addresses.SingleOrDefaultAsync(m => m.Id == id);
            if (addresses == null)
            {
                return NotFound();
            }

            _context.Addresses.Remove(addresses);
            await _context.SaveChangesAsync();

            return Ok(addresses);
        }

        private bool AddressesExists(int id)
        {
            return _context.Addresses.Any(e => e.Id == id);
        }
    }
}