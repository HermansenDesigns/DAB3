using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DAB2_2RDB;
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
        private readonly IUnitOfWork _unitOfWork;


        public PersonsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
            };

            foreach (var numbers in personCreation.PhoneNumberDtos)
            {
                person.PhoneNumbers.Add(new PhoneNumbers()
                {
                    Number = numbers.Number,
                    Person = person,
                    Usage = numbers.Type
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
        public async Task<IActionResult> PostPersons([FromBody] PersonCreationDto personCreation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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
//                PrimaryAddress = new PrimaryAddresses(),
            };

            foreach (var secondaryAddress in personCreation.SecondaryAddressDtos)
            {
                var address = new Addresses()
                {
                    AddressName = new AddressNames()
                    {
                        HouseNumber = secondaryAddress.AddressDto.Housenumber,
                        StreetName = secondaryAddress.AddressDto.StreetName
                    }
                };

                var addressType = new AddressTypes()
                {
                    Type = personCreation.Name,
                    Address = address
                };

                var personAddressType = new PersonAddressTypes()
                {
                    Person = person,
                    AddressType = addressType
                };

                var city = new Cities()
                {
                    Name = personCreation.PrimaryAddressDto.AddressDto.CityName,
                    ZipCode = personCreation.PrimaryAddressDto.AddressDto.Zip,
                };
                city.Addresses.Add(address);

                var countryCode = new CountryCodes()
                {
                    Cities = city,
                    Code = personCreation.PrimaryAddressDto.AddressDto.CountryCode
                };
            }

            //var primaryAddress = new PrimaryAddresses()
            //{
            //    AddressName = new AddressNames()
            //    {
            //        HouseNumber = personCreation.PrimaryAddressDto.AddressDto.Housenumber,
            //        StreetName = personCreation.PrimaryAddressDto.AddressDto.StreetName
            //    }
            //};

            //var primaryCity = new Cities()
            //{
            //    Name = personCreation.PrimaryAddressDto.AddressDto.CityName,
            //    ZipCode = personCreation.PrimaryAddressDto.AddressDto.Zip,
            //};
            //primaryCity.PrimaryAddresses.Add(primaryAddress);

            //var primaryCountryCode = new CountryCodes()
            //{
            //    Cities = primaryCity,
            //    Code = personCreation.PrimaryAddressDto.AddressDto.CountryCode
            //};

            foreach (var numbers in personCreation.PhoneNumberDtos)
            {
                person.PhoneNumbers.Add(new PhoneNumbers()
                {
                    Number = numbers.Number,
                    Usage = numbers.Type
                });
            }

            _context.Persons.Add(person);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPersons", new { id = person.Id }, person);
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