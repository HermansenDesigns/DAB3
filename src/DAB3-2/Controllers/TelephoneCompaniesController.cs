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
    [Route("api/TelephoneCompanies")]
    public class TelephoneCompaniesController : Controller
    {
        private readonly F184DABH2Gr24Context _context;

        public TelephoneCompaniesController(F184DABH2Gr24Context context)
        {
            _context = context;
        }

        // GET: api/TelephoneCompanies
        [HttpGet]
        public IEnumerable<TelephoneCompanies> GetTelephoneCompanies()
        {
            return _context.TelephoneCompanies;
        }

        // GET: api/TelephoneCompanies/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTelephoneCompanies([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var telephoneCompanies = await _context.TelephoneCompanies.SingleOrDefaultAsync(m => m.Id == id);

            if (telephoneCompanies == null)
            {
                return NotFound();
            }

            return Ok(telephoneCompanies);
        }

        // PUT: api/TelephoneCompanies/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTelephoneCompanies([FromRoute] int id, [FromBody] TelephoneCompanies telephoneCompanies)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != telephoneCompanies.Id)
            {
                return BadRequest();
            }

            _context.Entry(telephoneCompanies).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TelephoneCompaniesExists(id))
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

        // POST: api/TelephoneCompanies
        [HttpPost]
        public async Task<IActionResult> PostTelephoneCompanies([FromBody] TelephoneCompanies telephoneCompanies)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.TelephoneCompanies.Add(telephoneCompanies);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTelephoneCompanies", new { id = telephoneCompanies.Id }, telephoneCompanies);
        }

        // DELETE: api/TelephoneCompanies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTelephoneCompanies([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var telephoneCompanies = await _context.TelephoneCompanies.SingleOrDefaultAsync(m => m.Id == id);
            if (telephoneCompanies == null)
            {
                return NotFound();
            }

            _context.TelephoneCompanies.Remove(telephoneCompanies);
            await _context.SaveChangesAsync();

            return Ok(telephoneCompanies);
        }

        private bool TelephoneCompaniesExists(int id)
        {
            return _context.TelephoneCompanies.Any(e => e.Id == id);
        }
    }
}