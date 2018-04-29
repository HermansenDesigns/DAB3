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
        public async Task<IEnumerable<PersonUpdateDto>> GetPersons()
        {
            var people = _unitOfWork.PersonRepository
                .GetAllIncluding(persons => persons.PersonAddresses,
                    persons => persons.PersonAddressTypes,
                    persons => persons.PhoneNumbers,
                    persons => persons.PrimaryAddress);

            foreach (var person in people)
            {
                await IncludeAll(person);
            }

            List<PersonUpdateDto> dtos = ConvertToDto(people);

            return dtos;
            //return await _unitOfWork.PersonRepository.GetAllAsync();
        }


        // GET: api/Persons/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersons([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var person = await _unitOfWork.PersonRepository
                .GetAllIncluding(persons => persons.PersonAddresses,
                    persons => persons.PersonAddressTypes,
                    persons => persons.PhoneNumbers,
                    persons => persons.PrimaryAddress)
                        .FirstOrDefaultAsync(o => o.Id == id);

            await IncludeAll(person);


            if (person == null)
            {
                return NotFound();
            }

            var result = ConvertToDto(person);

            return Ok(result);
        }

        // PUT: api/Persons/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersons([FromRoute] int id, [FromBody] PersonUpdateDto personUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SplitName(personUpdate, out var name, out var middleName);

            await UpdatePerson(id, personUpdate, name, middleName);

            try
            {
                await _unitOfWork.SaveAsync();
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

            SplitName(personCreation, out var name, out var middleName);
            Persons person = await AddPerson(personCreation, name, middleName);

            await _unitOfWork.SaveAsync();

            return CreatedAtAction("GetPersons", new { id = person.Id }, ConvertToDto(person));
        }

        // DELETE: api/Persons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersons([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var persons = await _unitOfWork.PersonRepository.GetAsync(id);
            if (persons == null)
            {
                return NotFound();
            }
            _unitOfWork.PersonRepository.Delete(persons);
            await _unitOfWork.SaveAsync();

            return Ok(persons);
        }

        #region HelperMethods

        private async Task UpdatePerson(int id, PersonUpdateDto personUpdate, string[] name, string middleName)
        {
            // Create new person
            var person = new Persons
            {
                Id = personUpdate.Id,
                Context = personUpdate.Context,
                Email = personUpdate.Email,
                FirstName = name[0],
                MiddleName = middleName,
                LastName = name[name.Length - 1],
                PhoneNumbers = new List<PhoneNumbers>(),
                PrimaryAddress = new PrimaryAddresses(),
            };

            personUpdate.PhoneNumberDtos.ForEach(async c =>
            {
                var number = new PhoneNumbers()
                {
                    Id = c.Id,
                    PersonId = person.Id,
                    Number = c.Number,
                    Usage = c.Type
                };
                person.PhoneNumbers.Add(number);
                await _unitOfWork.PhoneNumberRepository.UpdateAsync(number, number.Id);
            });

            var telephoneCompany = new TelephoneCompanies()
            {
                CompanyName = "Telmore"
            };
            await _unitOfWork.TelephoneCompanyRepository.AddAsync(telephoneCompany);

            var addressTypes = await _unitOfWork.AddressTypeRepository.GetAllAsync();
            foreach (var addressType in addressTypes)
                _unitOfWork.AddressTypeRepository.Delete(addressType);

            var personAddressTypes = await _unitOfWork.PersonAddressTypeRepository.GetAllAsync();
            foreach (var personAddressType in personAddressTypes)
                _unitOfWork.PersonAddressTypeRepository.Delete(personAddressType);

            foreach (var secondaryAddress in personUpdate.SecondaryAddressDtos)
            {
                var address = new Addresses()
                {
                    Id = secondaryAddress.AddressDto.AddressId,
                    AddressName = new AddressNames()
                    {
                        Id = secondaryAddress.AddressNameId,
                        HouseNumber = secondaryAddress.AddressDto.Housenumber,
                        StreetName = secondaryAddress.AddressDto.StreetName
                    }
                };

                //var personAddress = new PersonAddresses()
                //{
                //    Address = address,
                //    Person = person
                //};
                //await _unitOfWork.PersonAddressRepository.AddAsync(personAddress);

                var addressType = new AddressTypes()
                {
                    Id = secondaryAddress.AddressTypeId,
                    Type = personUpdate.Name,
                    Address = address,
                };
                await _unitOfWork.AddressTypeRepository.UpdateAsync(addressType, addressType.Id);

                //var personAddressType = new PersonAddressTypes()
                //{
                //    Person = person,
                //    AddressType = addressType
                //};
                //await _unitOfWork.PersonAddressTypeRepository.AddAsync(personAddressType);

                var city = new Cities()
                {

                    Name = secondaryAddress.AddressDto.CityName,
                    ZipCode = secondaryAddress.AddressDto.Zip,
                };
                await _unitOfWork.CityRepository.UpdateAsync(city, city.Id);

                var countryCode = new CountryCodes()
                {
                    Id = secondaryAddress.AddressDto.CountryCodeId,
                    Code = secondaryAddress.AddressDto.CountryCode
                };
                await _unitOfWork.CountryCodeRepository.UpdateAsync(countryCode, countryCode.Id);
            }

            var primaryAddress = new PrimaryAddresses()
            {
                Id = personUpdate.PrimaryAddressDto.AddressDto.AddressId,
                AddressName = new AddressNames()
                {
                    Id = personUpdate.PrimaryAddressDto.AddressNameId,
                    HouseNumber = personUpdate.PrimaryAddressDto.AddressDto.Housenumber,
                    StreetName = personUpdate.PrimaryAddressDto.AddressDto.StreetName
                }
            };
            person.PrimaryAddress = primaryAddress;
            await _unitOfWork.PrimaryAddressRepository.UpdateAsync(primaryAddress, primaryAddress.Id);

            var primaryCity = new Cities()
            {
                Id = personUpdate.PrimaryAddressDto.AddressDto.CityId,
                Name = personUpdate.PrimaryAddressDto.AddressDto.CityName,
                ZipCode = personUpdate.PrimaryAddressDto.AddressDto.Zip,
            };
            primaryCity.PrimaryAddresses.Add(primaryAddress);
            await _unitOfWork.CityRepository.UpdateAsync(primaryCity, primaryCity.Id);

            var primaryCountryCode = new CountryCodes()
            {
                Id = personUpdate.PrimaryAddressDto.AddressDto.CountryCodeId,
                Code = personUpdate.PrimaryAddressDto.AddressDto.CountryCode
            };
            await _unitOfWork.CountryCodeRepository.UpdateAsync(primaryCountryCode, primaryCountryCode.Id);

            await _unitOfWork.PersonRepository.UpdateAsync(person, person.Id);
        }



        private async Task<Persons> AddPerson(PersonCreationDto personCreation, string[] name, string middleName)
        {
            // Create new person
            var person = new Persons
            {
                Context = personCreation.Context,
                Email = personCreation.Email,
                FirstName = name[0],
                MiddleName = middleName,
                LastName = name[name.Length - 1],
                PhoneNumbers = new List<PhoneNumbers>(),
                //PrimaryAddress = new PrimaryAddresses(),
            };

            personCreation.PhoneNumberDtos.ForEach(async c =>
            {
                var number = new PhoneNumbers()
                {
                    Number = c.Number,
                    Usage = c.Type
                };

                person.PhoneNumbers.Add(number);
                await _unitOfWork.PhoneNumberRepository.AddAsync(number);
            });

            var telephoneCompany = new TelephoneCompanies()
            {
                CompanyName = "Telmore"
            };
            await _unitOfWork.TelephoneCompanyRepository.AddAsync(telephoneCompany);

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

                var personAddress = new PersonAddresses()
                {
                    Address = address,
                    Person = person
                };
                await _unitOfWork.PersonAddressRepository.AddAsync(personAddress);

                var addressType = new AddressTypes()
                {
                    Type = personCreation.Name,
                    Address = address
                };
                await _unitOfWork.AddressTypeRepository.AddAsync(addressType);

                var personAddressType = new PersonAddressTypes()
                {
                    Person = person,
                    AddressType = addressType
                };
                await _unitOfWork.PersonAddressTypeRepository.AddAsync(personAddressType);

                var city = new Cities()
                {
                    Name = secondaryAddress.AddressDto.CityName,
                    ZipCode = secondaryAddress.AddressDto.Zip,
                };
                city.Addresses.Add(address);
                await _unitOfWork.CityRepository.AddAsync(city);

                var countryCode = new CountryCodes()
                {
                    Cities = city,
                    Code = secondaryAddress.AddressDto.CountryCode
                };
                await _unitOfWork.CountryCodeRepository.AddAsync(countryCode);
            }

            //var primaryAddress = new PrimaryAddresses()
            //{
            //    AddressName = new AddressNames()
            //    {
            //        HouseNumber = personCreation.PrimaryAddressDto.AddressDto.Housenumber,
            //        StreetName = personCreation.PrimaryAddressDto.AddressDto.StreetName
            //    }
            //};
            //person.PrimaryAddress = primaryAddress;
            //await _unitOfWork.PrimaryAddressRepository.AddAsync(primaryAddress);

            //var primaryCity = new Cities()
            //{
            //    Name = personCreation.PrimaryAddressDto.AddressDto.CityName,
            //    ZipCode = personCreation.PrimaryAddressDto.AddressDto.Zip,
            //};
            //primaryCity.PrimaryAddresses.Add(primaryAddress);
            //await _unitOfWork.CityRepository.AddAsync(primaryCity);

            //var primaryCountryCode = new CountryCodes()
            //{
            //    Cities = primaryCity,
            //    Code = personCreation.PrimaryAddressDto.AddressDto.CountryCode
            //};
            //await _unitOfWork.CountryCodeRepository.AddAsync(primaryCountryCode);

            await _unitOfWork.PersonRepository.AddAsync(person);
            return person;
        }

        private static void SplitName(PersonCreationDto personCreation, out string[] name, out string middleName)
        {
            // Split name
            name = personCreation.Name.Split(' ');
            StringBuilder middleNameBuilder = new StringBuilder();
            // Make Middle Name
            for (var index = 1; index < name.Length - 1; index++)
            {
                var s = name[index];
                middleNameBuilder.Append(s);
                if (s != name[name.Length - 2])
                    middleNameBuilder.Append(" ");
            }

            middleName = middleNameBuilder.ToString();
        }

        private static void SplitName(PersonUpdateDto personCreation, out string[] name, out string middleName)
        {
            // Split name
            name = personCreation.Name.Split(' ');
            StringBuilder middleNameBuilder = new StringBuilder();
            // Make Middle Name
            for (var index = 1; index < name.Length - 1; index++)
            {
                var s = name[index];
                middleNameBuilder.Append(s);
                if (s != name[name.Length - 2])
                    middleNameBuilder.Append(" ");
            }

            middleName = middleNameBuilder.ToString();
        }

        private bool PersonsExists(int id)
        {
            return _unitOfWork.PersonRepository.Get(id) != null;
        }
        private async Task IncludeAll(Persons person)
        {
            person.PersonAddresses.Clear();
            person.PersonAddresses = _unitOfWork.PersonAddressRepository.GetAllIncluding(addresses => addresses.Address).Where(o => o.PersonId == person.Id).ToList();

            foreach (var address in person.PersonAddresses)
            {
                address.Address.AddressName = await _unitOfWork.AddressNameRepository
                    .GetAllIncluding(names => names.Addresses, names => names.PrimaryAddresses)
                    .FirstOrDefaultAsync(o => o.Id == address.Address.AddressNameId);

                address.Address.City = await _unitOfWork.CityRepository
                    .GetAllIncluding(cities => cities.Addresses, cities => cities.CountryCode,
                        cities => cities.PrimaryAddresses)
                    .FirstOrDefaultAsync(o => o.Id == address.Address.CityId);
            }

            person.PersonAddressTypes.Clear();
            person.PersonAddressTypes = _unitOfWork.PersonAddressTypeRepository.GetAllIncluding(types => types.AddressType).Where(o => o.PersonId == person.Id).ToList();

            foreach (var addressType in person.PersonAddressTypes)
            {
                addressType.AddressType = await _unitOfWork.AddressTypeRepository
                    .GetAllIncluding(types => types.Address, types => types.PersonAddressTypes)
                    .FirstOrDefaultAsync(o => o.Id == addressType.AddressType.AddressId);
            }

            person.PhoneNumbers.Clear();
            person.PhoneNumbers = _unitOfWork.PhoneNumberRepository.GetAllIncluding(numbers => numbers.TelephoneCompany).Where(o => o.PersonId == person.Id).ToList();

            foreach (var phoneNumber in person.PhoneNumbers)
            {
                phoneNumber.TelephoneCompany = await _unitOfWork.TelephoneCompanyRepository
                    .GetAllIncluding(companies => companies.PhoneNumbers)
                    .FirstOrDefaultAsync(o => o.Id == phoneNumber.TelephoneCompanyId);
            }
        }
        private static List<PersonUpdateDto> ConvertToDto(IQueryable<Persons> people)
        {
            var dtos = new List<PersonUpdateDto>();

            foreach (var person in people)
            {
                if (person == null)
                    continue;

                var personUpdate = new PersonUpdateDto()
                {
                    Context = person.Context,
                    Email = person.Email,
                    Id = person.Id,
                    Name = $"{person.FirstName} {person.MiddleName} {person.LastName}",
                    PhoneNumberDtos = new List<PhoneNumberUpdateDto>(),

                    SecondaryAddressDtos = new List<SecondaryAddressUpdateDto>()
                };

                if (person.PrimaryAddress != null)
                {
                    personUpdate.PrimaryAddressDto = new PrimaryAddressUpdateDto
                    {
                        Id = person.PrimaryAddressId.GetValueOrDefault(0),
                        AddressNameId = person.PrimaryAddress.AddressNameId.GetValueOrDefault(0)
                    };

                    if (person.PrimaryAddress.AddressName != null && person.PrimaryAddress.City != null)
                    {
                        personUpdate.PrimaryAddressDto.AddressDto = new AddressUpdateDto
                        {
                            AddressId = person.PrimaryAddressId.GetValueOrDefault(0),
                            Housenumber = person.PrimaryAddress.AddressName.HouseNumber,
                            StreetName = person.PrimaryAddress.AddressName.StreetName,
                            CityId = person.PrimaryAddress.CityId,
                            CountryCodeId = person.PrimaryAddress.City.CountryCodeId,
                            Zip = person.PrimaryAddress.City.ZipCode,
                            CountryCode = person.PrimaryAddress.City.CountryCode.Code,
                            CityName = person.PrimaryAddress.City.Name
                        };
                    }
                }


                foreach (var address in person.PersonAddresses)
                {
                    if (address == null)
                        continue;

                    personUpdate.SecondaryAddressDtos.Add(new SecondaryAddressUpdateDto()
                    {
                        AddressDto = new AddressUpdateDto()
                        {
                            AddressId = address.Address.Id,
                            Housenumber = address.Address.AddressName.HouseNumber,
                            StreetName = address.Address.AddressName.StreetName,
                            CityId = address.Address.CityId,
                            CountryCodeId = address.Address.City.CountryCodeId,
                            Zip = address.Address.City.ZipCode,
                            CountryCode = address.Address.City.CountryCode.Code,
                            CityName = address.Address.City.Name,
                        },
                        AddressNameId = address.Address.AddressNameId.GetValueOrDefault(0),
                    });
                }

                foreach (var phoneNumber in person.PhoneNumbers)
                {
                    if (phoneNumber == null)
                        continue;
                    personUpdate.PhoneNumberDtos.Add(new PhoneNumberUpdateDto()
                    {
                        Id = phoneNumber.Id,
                        Number = phoneNumber.Number,
                        Type = phoneNumber.Usage
                    });
                }

                dtos.Add(personUpdate);
            }

            return dtos;
        }
        private static PersonUpdateDto ConvertToDto(Persons person)
        {
            if (person == null)
                return null;

            var personUpdate = new PersonUpdateDto()
            {
                Context = person.Context,
                Email = person.Email,
                Id = person.Id,
                Name = $"{person.FirstName} {person.MiddleName} {person.LastName}",
                PhoneNumberDtos = new List<PhoneNumberUpdateDto>(),

                SecondaryAddressDtos = new List<SecondaryAddressUpdateDto>()
            };

            if (person.PrimaryAddress != null)
            {
                personUpdate.PrimaryAddressDto = new PrimaryAddressUpdateDto
                {
                    Id = person.PrimaryAddressId.GetValueOrDefault(0),
                    AddressNameId = person.PrimaryAddress.AddressNameId.GetValueOrDefault(0)
                };

                if (person.PrimaryAddress.AddressName != null && person.PrimaryAddress.City != null)
                {
                    personUpdate.PrimaryAddressDto.AddressDto = new AddressUpdateDto
                    {
                        AddressId = person.PrimaryAddressId.GetValueOrDefault(0),
                        Housenumber = person.PrimaryAddress.AddressName.HouseNumber,
                        StreetName = person.PrimaryAddress.AddressName.StreetName,
                        CityId = person.PrimaryAddress.CityId,
                        CountryCodeId = person.PrimaryAddress.City.CountryCodeId,
                        Zip = person.PrimaryAddress.City.ZipCode,
                        CountryCode = person.PrimaryAddress.City.CountryCode.Code,
                        CityName = person.PrimaryAddress.City.Name
                    };
                }
            }


            foreach (var address in person.PersonAddresses)
            {
                if (address == null)
                    continue;

                personUpdate.SecondaryAddressDtos.Add(new SecondaryAddressUpdateDto()
                {
                    AddressDto = new AddressUpdateDto()
                    {
                        AddressId = address.Address.Id,
                        Housenumber = address.Address.AddressName.HouseNumber,
                        StreetName = address.Address.AddressName.StreetName,
                        CityId = address.Address.CityId,
                        CountryCodeId = address.Address.City.CountryCodeId,
                        Zip = address.Address.City.ZipCode,
                        CountryCode = address.Address.City.CountryCode.Code,
                        CityName = address.Address.City.Name,
                    },
                    AddressNameId = address.Address.AddressNameId.GetValueOrDefault(0),
                });
            }

            foreach (var phoneNumber in person.PhoneNumbers)
            {
                if (phoneNumber == null)
                    continue;
                personUpdate.PhoneNumberDtos.Add(new PhoneNumberUpdateDto()
                {
                    Id = phoneNumber.Id,
                    Number = phoneNumber.Number,
                    Type = phoneNumber.Usage
                });
            }

            return personUpdate;
        }


        #endregion

    }
}