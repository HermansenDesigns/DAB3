using DAB2_2RDB;
using DAB32.Models;

namespace DAB32.Data.Repositories
{
    public class PrimaryAddressRepository : GenericRepository<PrimaryAddresses>, IPrimaryAddressRepository
    {
        public PrimaryAddressRepository(F184DABH2Gr24Context context) : base(context)
        {

        }
    }
}