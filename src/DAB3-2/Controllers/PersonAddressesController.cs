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
    [Route("api/PersonAddresses")]
    public class PersonAddressesController : Controller
    {
        private readonly F184DABH2Gr24Context _context;

        public PersonAddressesController(F184DABH2Gr24Context context)
        {
            _context = context;
        }

        // GET: api/PersonAddresses
        [HttpGet]
        public IEnumerable<PersonAddresses> GetPersonAddresses()
        {
            return _context.PersonAddresses;
        }

        // GET: api/PersonAddresses/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonAddresses([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var personAddresses = await _context.PersonAddresses.SingleOrDefaultAsync(m => m.PersonId == id);

            if (personAddresses == null)
            {
                return NotFound();
            }

            return Ok(personAddresses);
        }

        // PUT: api/PersonAddresses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonAddresses([FromRoute] int id, [FromBody] PersonAddresses personAddresses)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != personAddresses.PersonId)
            {
                return BadRequest();
            }

            _context.Entry(personAddresses).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonAddressesExists(id))
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

        // POST: api/PersonAddresses
        [HttpPost]
        public async Task<IActionResult> PostPersonAddresses([FromBody] PersonAddresses personAddresses)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.PersonAddresses.Add(personAddresses);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PersonAddressesExists(personAddresses.PersonId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPersonAddresses", new { id = personAddresses.PersonId }, personAddresses);
        }

        // DELETE: api/PersonAddresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonAddresses([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var personAddresses = await _context.PersonAddresses.SingleOrDefaultAsync(m => m.PersonId == id);
            if (personAddresses == null)
            {
                return NotFound();
            }

            _context.PersonAddresses.Remove(personAddresses);
            await _context.SaveChangesAsync();

            return Ok(personAddresses);
        }

        private bool PersonAddressesExists(int id)
        {
            return _context.PersonAddresses.Any(e => e.PersonId == id);
        }
    }
}