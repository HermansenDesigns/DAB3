using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using DAB3_3.Data;
using DAB3_3.Data.Models;
using System.Net;

namespace DAB3_3.Controllers
{
    public class PersonController : ApiController
    {
        //private readonly UnitOfWork uow;
        public async Task<IEnumerable<Person>> GetPeople()
        {
            return await DocumentDBRepository<Person>.ReadAll(p => true);
        }

        //public PersonController()
        //{
        //    uow = new UnitOfWork();
        //}
        /*

        // GET: api/Person
        public IEnumerable<Person> Get()
        {
            return uow.GetAllPersons();
        }

        // GET: api/Person/5
        public Person Get(string id)
        {
            return uow.GetPerson(id);
        }

        // POST: api/Person
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Person/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Person/5
        public void Delete(int id)
        {
        }*/
    }
}
