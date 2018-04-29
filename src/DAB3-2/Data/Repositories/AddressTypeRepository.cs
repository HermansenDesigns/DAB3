using DAB2_2RDB;
using DAB32.Models;

namespace DAB32.Data.Repositories
{
    public class AddressTypeRepository : GenericRepository<AddressTypes>, IAddressTypeRepository
    {
        public AddressTypeRepository(F184DABH2Gr24Context context) : base(context)
        {

        }
    }
}