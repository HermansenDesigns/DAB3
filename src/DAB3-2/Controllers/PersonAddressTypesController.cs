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
    [Route("api/PersonAddressTypes")]
    public class PersonAddressTypesController : Controller
    {
        private readonly F184DABH2Gr24Context _context;

        public PersonAddressTypesController(F184DABH2Gr24Context context)
        {
            _context = context;
        }

        // GET: api/PersonAddressTypes
        [HttpGet]
        public IEnumerable<PersonAddressTypes> GetPersonAddressTypes()
        {
            return _context.PersonAddressTypes;
        }

        // GET: api/PersonAddressTypes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonAddressTypes([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var personAddressTypes = await _context.PersonAddressTypes.SingleOrDefaultAsync(m => m.PersonId == id);

            if (personAddressTypes == null)
            {
                return NotFound();
            }

            return Ok(personAddressTypes);
        }

        // PUT: api/PersonAddressTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonAddressTypes([FromRoute] int id, [FromBody] PersonAddressTypes personAddressTypes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != personAddressTypes.PersonId)
            {
                return BadRequest();
            }

            _context.Entry(personAddressTypes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonAddressTypesExists(id))
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

        // POST: api/PersonAddressTypes
        [HttpPost]
        public async Task<IActionResult> PostPersonAddressTypes([FromBody] PersonAddressTypes personAddressTypes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.PersonAddressTypes.Add(personAddressTypes);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PersonAddressTypesExists(personAddressTypes.PersonId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPersonAddressTypes", new { id = personAddressTypes.PersonId }, personAddressTypes);
        }

        // DELETE: api/PersonAddressTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonAddressTypes([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var personAddressTypes = await _context.PersonAddressTypes.SingleOrDefaultAsync(m => m.PersonId == id);
            if (personAddressTypes == null)
            {
                return NotFound();
            }

            _context.PersonAddressTypes.Remove(personAddressTypes);
            await _context.SaveChangesAsync();

            return Ok(personAddressTypes);
        }

        private bool PersonAddressTypesExists(int id)
        {
            return _context.PersonAddressTypes.Any(e => e.PersonId == id);
        }
    }
}