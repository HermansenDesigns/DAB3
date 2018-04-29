using DAB2_2RDB;
using DAB32.Models;

namespace DAB32.Data.Repositories
{
    public class PhoneNumberRepository : GenericRepository<PhoneNumbers>, IPhoneNumberRepository
    {
        public PhoneNumberRepository(F184DABH2Gr24Context context) : base(context)
        {

        }
    }
}