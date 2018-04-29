using DAB2_2RDB;
using DAB32.Models;

namespace DAB32.Data.Repositories
{
    public class CityRepository : GenericRepository<Cities>, ICityRepository
    {
        public CityRepository(F184DABH2Gr24Context context) : base(context)
        {

        }
    }
}