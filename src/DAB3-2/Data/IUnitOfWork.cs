using System.Threading.Tasks;
using DAB32.Models;

namespace DAB2_2RDB
{
    public interface IUnitOfWork
    {
        Repository<Addresses> AddressRepository { get; set; }
        Repository<AddressTypes> AddressTypeRepository { get; set; }
        Repository<Cities> CityRepository { get; set; }
        F184DABH2Gr24Context Context { get; set; }
        Repository<CountryCodes> CountryCodeRepository { get; set; }
        UnionRepository<PersonAddresses> PersonAddressRepository { get; set; }
        UnionRepository<PersonAddressTypes> PersonAddressTypeRepository { get; set; }
        Repository<Persons> PersonRepository { get; set; }
        Repository<TelephoneCompanies> TelephoneCompanyRepository { get; set; }

        void Save();
        Task SaveAsync();
    }
}