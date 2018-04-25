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
    [Route("api/AddressTypes")]
    public class AddressTypesController : Controller
    {
        private readonly F184DABH2Gr24Context _context;

        public AddressTypesController(F184DABH2Gr24Context context)
        {
            _context = context;
        }

        // GET: api/AddressTypes
        [HttpGet]
        public IEnumerable<AddressTypes> GetAddressTypes()
        {
            return _context.AddressTypes;
        }

        // GET: api/AddressTypes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAddressTypes([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var addressTypes = await _context.AddressTypes.SingleOrDefaultAsync(m => m.Id == id);

            if (addressTypes == null)
            {
                return NotFound();
            }

            return Ok(addressTypes);
        }

        // PUT: api/AddressTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddressTypes([FromRoute] int id, [FromBody] AddressTypes addressTypes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != addressTypes.Id)
            {
                return BadRequest();
            }

            _context.Entry(addressTypes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressTypesExists(id))
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

        // POST: api/AddressTypes
        [HttpPost]
        public async Task<IActionResult> PostAddressTypes([FromBody] AddressTypes addressTypes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.AddressTypes.Add(addressTypes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAddressTypes", new { id = addressTypes.Id }, addressTypes);
        }

        // DELETE: api/AddressTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddressTypes([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var addressTypes = await _context.AddressTypes.SingleOrDefaultAsync(m => m.Id == id);
            if (addressTypes == null)
            {
                return NotFound();
            }

            _context.AddressTypes.Remove(addressTypes);
            await _context.SaveChangesAsync();

            return Ok(addressTypes);
        }

        private bool AddressTypesExists(int id)
        {
            return _context.AddressTypes.Any(e => e.Id == id);
        }
    }
}