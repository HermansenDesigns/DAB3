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
    [Route("api/Persons")]
    public class PersonsController : Controller
    {
        private readonly F184DABH2Gr24Context _context;

        public PersonsController(F184DABH2Gr24Context context)
        {
            _context = context;
        }

        // GET: api/Persons
        [HttpGet]
        public IEnumerable<Persons> GetPersons()
        {
            return _context.Persons;
        }

        // GET: api/Persons/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersons([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var persons = await _context.Persons.SingleOrDefaultAsync(m => m.Id == id);

            if (persons == null)
            {
                return NotFound();
            }

            return Ok(persons);
        }

        // PUT: api/Persons/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersons([FromRoute] int id, [FromBody] Persons persons)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != persons.Id)
            {
                return BadRequest();
            }

            _context.Entry(persons).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonsExists(id))
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

        // POST: api/Persons
        [HttpPost]
        public async Task<IActionResult> PostPersons([FromBody] Persons persons)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Persons.Add(persons);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPersons", new { id = persons.Id }, persons);
        }

        // DELETE: api/Persons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersons([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var persons = await _context.Persons.SingleOrDefaultAsync(m => m.Id == id);
            if (persons == null)
            {
                return NotFound();
            }

            _context.Persons.Remove(persons);
            await _context.SaveChangesAsync();

            return Ok(persons);
        }

        private bool PersonsExists(int id)
        {
            return _context.Persons.Any(e => e.Id == id);
        }
    }
}