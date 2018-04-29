using System.Threading.Tasks;
using DAB32.Data.Repositories;
using DAB32.Models;

namespace DAB2_2RDB
{
    public interface IUnitOfWork
    {
        IPersonRepository PersonRepository { get; set; }
        ITelephoneCompanyRepository TelephoneCompanyRepository { get; set; }
        IAddressRepository AddressRepository { get; set; }
        IAddressTypeRepository AddressTypeRepository { get; set; }
        ICityRepository CityRepository { get; set; }
        ICountryCodeRepository CountryCodeRepository { get; set; }
        IPhoneNumberRepository PhoneNumberRepository { get; set; }
        IPrimaryAddressRepository PrimaryAddressRepository { get; set; }
        IAddressNameRepository AddressNameRepository { get; set; }

        // Join table repos
        IPersonAddressRepository PersonAddressRepository { get; set; }
        IPersonAddressTypeRepository PersonAddressTypeRepository { get; set; }

        void Save();
        Task SaveAsync();
    }
}