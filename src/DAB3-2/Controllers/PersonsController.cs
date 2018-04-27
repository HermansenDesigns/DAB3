using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAB32.Models;
using DAB32.Models.Resources;

namespace DAB3_2.Controllers
{
    [Produces("application/json")]
    [Route("api/Persons")]
    public class PersonsController : Controller
    {
        private readonly F184DABH2Gr24Context _context;
        private readonly IMapper _mapper;


        public PersonsController(F184DABH2Gr24Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
        public async Task<IActionResult> PutPersons([FromRoute] int id, [FromBody] PersonCreationDto personCreation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var toEdit = _context.Persons.SingleOrDefault(o => o.Id == id);

            // Split name
            var name = personCreation.Name.Split(' ');
            StringBuilder middleNameBuilder = new StringBuilder();
            // Make Middle Name
            foreach (var s in name)
                if (s != name[0] || s != name[name.Length - 1])
                {
                    middleNameBuilder.Append(s);
                    if (s != name[name.Length - 2])
                        middleNameBuilder.Append(" ");
                }
            var middleName = middleNameBuilder.ToString();


            // Create new person
            var person = new Persons
            {
                Context = personCreation.Context,
                Email = personCreation.Email,
                FirstName = name[0],
                MiddleName = middleName,
                LastName = name[name.Length - 1],
                PersonAddresses = new List<PersonAddresses>(),
                PersonAddressTypes = new List<PersonAddressTypes>(),
                PhoneNumbers = new List<PhoneNumbers>(),
                PrimaryAddress = new PrimaryAddresses(),
            };

            foreach (var numbers in personCreation.PhoneNumberDtos)
            {
                person.PhoneNumbers.Add( new PhoneNumbers()
                {
                    Number = numbers.Number,
                    Person = person,
                    Usage = numbers.Type
                });
            }

            foreach (var secondaryAddress in personCreation.SecondaryAddressDtos)
            {
                person.PersonAddresses.Add(new Addresses()
                {

                });
            }


            _context.Entry<Persons>(toEdit).State = EntityState.Modified;

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
        public async Task<IActionResult> PostPersons([FromBody] PersonCreationDto persons)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var person = _mapper.Map<Persons>(persons);

            _context.Persons.Add(person);
            await _context.SaveChangesAsync();

            var result = _mapper.Map<PersonDto>(person);

            return CreatedAtAction("GetPersons", new { id = result.Id }, result);
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