using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DAB3_3.Lib;
using DAB3_3.Lib.Model;

namespace DAB3_3.Controllers
{
    public class PersonController : ApiController
    {
        private readonly UnitOfWork uow;

        public PersonController()
        {
            uow = new UnitOfWork();
        }

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
        }
    }
}
