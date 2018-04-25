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
    [Route("api/CountryCodes")]
    public class CountryCodesController : Controller
    {
        private readonly F184DABH2Gr24Context _context;

        public CountryCodesController(F184DABH2Gr24Context context)
        {
            _context = context;
        }

        // GET: api/CountryCodes
        [HttpGet]
        public IEnumerable<CountryCodes> GetCountryCodes()
        {
            return _context.CountryCodes;
        }

        // GET: api/CountryCodes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCountryCodes([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var countryCodes = await _context.CountryCodes.SingleOrDefaultAsync(m => m.Id == id);

            if (countryCodes == null)
            {
                return NotFound();
            }

            return Ok(countryCodes);
        }

        // PUT: api/CountryCodes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountryCodes([FromRoute] int id, [FromBody] CountryCodes countryCodes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != countryCodes.Id)
            {
                return BadRequest();
            }

            _context.Entry(countryCodes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryCodesExists(id))
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

        // POST: api/CountryCodes
        [HttpPost]
        public async Task<IActionResult> PostCountryCodes([FromBody] CountryCodes countryCodes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CountryCodes.Add(countryCodes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCountryCodes", new { id = countryCodes.Id }, countryCodes);
        }

        // DELETE: api/CountryCodes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountryCodes([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var countryCodes = await _context.CountryCodes.SingleOrDefaultAsync(m => m.Id == id);
            if (countryCodes == null)
            {
                return NotFound();
            }

            _context.CountryCodes.Remove(countryCodes);
            await _context.SaveChangesAsync();

            return Ok(countryCodes);
        }

        private bool CountryCodesExists(int id)
        {
            return _context.CountryCodes.Any(e => e.Id == id);
        }
    }
}