using System.Collections.Generic;
using DAB2_2RDB;
using DAB32.Models;

namespace DAB32.Data.Repositories
{
    public class PersonAddressRepository : GenericRepository<PersonAddresses>, IPersonAddressRepository
    {
        public PersonAddressRepository(F184DABH2Gr24Context context) : base(context)
        {

        }
    }
}