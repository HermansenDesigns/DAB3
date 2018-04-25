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
    [Route("api/PhoneNumbers")]
    public class PhoneNumbersController : Controller
    {
        private readonly F184DABH2Gr24Context _context;

        public PhoneNumbersController(F184DABH2Gr24Context context)
        {
            _context = context;
        }

        // GET: api/PhoneNumbers
        [HttpGet]
        public IEnumerable<PhoneNumbers> GetPhoneNumbers()
        {
            return _context.PhoneNumbers;
        }

        // GET: api/PhoneNumbers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPhoneNumbers([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var phoneNumbers = await _context.PhoneNumbers.SingleOrDefaultAsync(m => m.Id == id);

            if (phoneNumbers == null)
            {
                return NotFound();
            }

            return Ok(phoneNumbers);
        }

        // PUT: api/PhoneNumbers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPhoneNumbers([FromRoute] int id, [FromBody] PhoneNumbers phoneNumbers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != phoneNumbers.Id)
            {
                return BadRequest();
            }

            _context.Entry(phoneNumbers).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhoneNumbersExists(id))
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

        // POST: api/PhoneNumbers
        [HttpPost]
        public async Task<IActionResult> PostPhoneNumbers([FromBody] PhoneNumbers phoneNumbers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.PhoneNumbers.Add(phoneNumbers);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPhoneNumbers", new { id = phoneNumbers.Id }, phoneNumbers);
        }

        // DELETE: api/PhoneNumbers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhoneNumbers([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var phoneNumbers = await _context.PhoneNumbers.SingleOrDefaultAsync(m => m.Id == id);
            if (phoneNumbers == null)
            {
                return NotFound();
            }

            _context.PhoneNumbers.Remove(phoneNumbers);
            await _context.SaveChangesAsync();

            return Ok(phoneNumbers);
        }

        private bool PhoneNumbersExists(int id)
        {
            return _context.PhoneNumbers.Any(e => e.Id == id);
        }
    }
}