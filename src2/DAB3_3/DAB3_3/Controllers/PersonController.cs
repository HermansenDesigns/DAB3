using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using DAB3_3.Data;
using DAB3_3.Data.Models;
using System.Net;
using System.Web.Http.Description;
//this Solution is made with inspiration from :https://docs.microsoft.com/da-dk/azure/cosmos-db/sql-api-dotnetcore-get-started
namespace DAB3_3.Controllers
{
    public class PersonController : ApiController
    {
        
        // GET: api/Person
        [ResponseType(typeof(Person))]
        public async Task<IEnumerable<Person>> GetPeople()
        { 
            return await DocumentDBRepository<Person>.Read(p => true);
        }

        [ResponseType(typeof(Person))]
        public async Task<IHttpActionResult> GetById(string id)
        {
            var person = await DocumentDBRepository<Person>.Read(p => p.Id == id);

            if (!person.Any())
                return NotFound();

            return Ok(person);
        }

        [ResponseType(typeof(Person))]
        public async Task<IHttpActionResult> PutPerson(Person person)
        {
            return await DocumentDBRepository<Person>.Update(person);
        }
        [ResponseType(typeof(Person))]
        public async Task<IHttpActionResult> PostPerson(Person person)
        {
            return await DocumentDBRepository<Person>.Create(person);
        }

        public async Task<IHttpActionResult> DeletePerson(string id)
        {
            return await DocumentDBRepository<Person>.Delete(id);
        }

    }
}
