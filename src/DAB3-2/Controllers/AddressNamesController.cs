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
    [Route("api/AddressNames")]
    public class AddressNamesController : Controller
    {
        private readonly F184DABH2Gr24Context _context;

        public AddressNamesController(F184DABH2Gr24Context context)
        {
            _context = context;
        }

        // GET: api/AddressNames
        [HttpGet]
        public IEnumerable<AddressNames> GetAddressNames()
        {
            return _context.AddressNames;
        }

        // GET: api/AddressNames/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAddressNames([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var addressNames = await _context.AddressNames.SingleOrDefaultAsync(m => m.Id == id);

            if (addressNames == null)
            {
                return NotFound();
            }

            return Ok(addressNames);
        }

        // PUT: api/AddressNames/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddressNames([FromRoute] int id, [FromBody] AddressNames addressNames)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != addressNames.Id)
            {
                return BadRequest();
            }

            _context.Entry(addressNames).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressNamesExists(id))
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

        // POST: api/AddressNames
        [HttpPost]
        public async Task<IActionResult> PostAddressNames([FromBody] AddressNames addressNames)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.AddressNames.Add(addressNames);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAddressNames", new { id = addressNames.Id }, addressNames);
        }

        // DELETE: api/AddressNames/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddressNames([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var addressNames = await _context.AddressNames.SingleOrDefaultAsync(m => m.Id == id);
            if (addressNames == null)
            {
                return NotFound();
            }

            _context.AddressNames.Remove(addressNames);
            await _context.SaveChangesAsync();

            return Ok(addressNames);
        }

        private bool AddressNamesExists(int id)
        {
            return _context.AddressNames.Any(e => e.Id == id);
        }
    }
}