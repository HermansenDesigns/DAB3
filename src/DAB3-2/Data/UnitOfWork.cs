using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAB32.Models;
using Microsoft.EntityFrameworkCore;

namespace DAB2_2RDB
{
    public class UnitOfWork : IUnitOfWork
    {
        public F184DABH2Gr24Context Context { get; set; }

        // Single entity Repos
        public Repository<Persons> PersonRepository { get; set; }
        public Repository<TelephoneCompanies> TelephoneCompanyRepository { get; set; }
        public Repository<Addresses> AddressRepository { get; set; }
        public Repository<AddressTypes> AddressTypeRepository { get; set; }
        public Repository<Cities> CityRepository { get; set; }
        public Repository<CountryCodes> CountryCodeRepository { get; set; }

        // Join table repos
        public UnionRepository<PersonAddresses> PersonAddressRepository { get; set; }
        public UnionRepository<PersonAddressTypes> PersonAddressTypeRepository { get; set; }


        public UnitOfWork(F184DABH2Gr24Context context)
        {
            Context = context;

            AddressRepository = new Repository<Addresses>(context);
            AddressTypeRepository = new Repository<AddressTypes>(context);
            CityRepository = new Repository<Cities>(context);
            CountryCodeRepository = new Repository<CountryCodes>(context);
            PersonAddressRepository = new UnionRepository<PersonAddresses>(context);
            PersonAddressTypeRepository = new UnionRepository<PersonAddressTypes>(context);
            PersonRepository = new Repository<Persons>(context);
            CountryCodeRepository = new Repository<CountryCodes>(context);
            TelephoneCompanyRepository = new Repository<TelephoneCompanies>(context);
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
