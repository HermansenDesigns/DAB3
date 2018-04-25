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
    [Route("api/PrimaryAddresses")]
    public class PrimaryAddressesController : Controller
    {
        private readonly F184DABH2Gr24Context _context;

        public PrimaryAddressesController(F184DABH2Gr24Context context)
        {
            _context = context;
        }

        // GET: api/PrimaryAddresses
        [HttpGet]
        public IEnumerable<PrimaryAddresses> GetPrimaryAddresses()
        {
            return _context.PrimaryAddresses;
        }

        // GET: api/PrimaryAddresses/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPrimaryAddresses([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var primaryAddresses = await _context.PrimaryAddresses.SingleOrDefaultAsync(m => m.Id == id);

            if (primaryAddresses == null)
            {
                return NotFound();
            }

            return Ok(primaryAddresses);
        }

        // PUT: api/PrimaryAddresses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrimaryAddresses([FromRoute] int id, [FromBody] PrimaryAddresses primaryAddresses)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != primaryAddresses.Id)
            {
                return BadRequest();
            }

            _context.Entry(primaryAddresses).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrimaryAddressesExists(id))
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

        // POST: api/PrimaryAddresses
        [HttpPost]
        public async Task<IActionResult> PostPrimaryAddresses([FromBody] PrimaryAddresses primaryAddresses)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.PrimaryAddresses.Add(primaryAddresses);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPrimaryAddresses", new { id = primaryAddresses.Id }, primaryAddresses);
        }

        // DELETE: api/PrimaryAddresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrimaryAddresses([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var primaryAddresses = await _context.PrimaryAddresses.SingleOrDefaultAsync(m => m.Id == id);
            if (primaryAddresses == null)
            {
                return NotFound();
            }

            _context.PrimaryAddresses.Remove(primaryAddresses);
            await _context.SaveChangesAsync();

            return Ok(primaryAddresses);
        }

        private bool PrimaryAddressesExists(int id)
        {
            return _context.PrimaryAddresses.Any(e => e.Id == id);
        }
    }
}