using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAB32.Data.Repositories;
using DAB32.Models;
using Microsoft.EntityFrameworkCore;

namespace DAB2_2RDB
{
    public class UnitOfWork : IUnitOfWork
    {
        public F184DABH2Gr24Context Context { get; set; }

        // Single entity Repos
        public IPersonRepository PersonRepository { get; set; }
        public ITelephoneCompanyRepository TelephoneCompanyRepository { get; set; }
        public IAddressRepository AddressRepository { get; set; }
        public IAddressTypeRepository AddressTypeRepository { get; set; }
        public ICityRepository CityRepository { get; set; }
        public ICountryCodeRepository CountryCodeRepository { get; set; }
        public IPhoneNumberRepository PhoneNumberRepository { get; set; }
        public IPrimaryAddressRepository PrimaryAddressRepository { get; set; }
        public IAddressNameRepository AddressNameRepository { get; set; }

        // Join table repos
        public IPersonAddressRepository PersonAddressRepository { get; set; }
        public IPersonAddressTypeRepository PersonAddressTypeRepository { get; set; }


        public UnitOfWork(F184DABH2Gr24Context context)
        {
            Context = context;

            AddressRepository = new AddressRepository(context);
            AddressTypeRepository = new AddressTypeRepository(context);
            CityRepository = new CityRepository(context);
            CountryCodeRepository = new CountryCodeRepository(context);
            PersonAddressRepository = new PersonAddressRepository(context);
            PersonAddressTypeRepository = new PersonAddressTypeRepository(context);
            PersonRepository = new PersonRepository(context);
            CountryCodeRepository = new CountryCodeRepository(context);
            TelephoneCompanyRepository = new TelephoneCompanyRepository(context);
            PhoneNumberRepository = new PhoneNumberRepository(context);
            PrimaryAddressRepository = new PrimaryAddressRepository(context);
            AddressNameRepository = new AddressNameRepository(context);
    }

    public async Task SaveAsync()
        {
            await Context.SaveChangesAsync();
        }

        public void Save()
        {
            Context.SaveChanges();
        }
    }
}
