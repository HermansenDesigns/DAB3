using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAB2_2RDB;
using DAB32.Models;

namespace DAB32.Data.Repositories
{
    public class AddressRepository : GenericRepository<Addresses>, IAddressRepository
    {
        public AddressRepository(F184DABH2Gr24Context context) : base(context)
        {
        }
    }
}
