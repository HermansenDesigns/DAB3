using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAB3_3.Data.Models;
using DocumentDB.Repository;
using Microsoft.Azure.Documents.Client.TransientFaultHandling;

namespace DAB3_3.Data
{
    public class UnitOfWork : IDisposable
    {
        public IReliableReadWriteDocumentClient Client { get; set; }
        private readonly DocumentDbRepository<Person> _repo;
        private List<Person> NewOnes = new List<Person>();
        private List<Person> UpdatedOnes = new List<Person>();
        private List<Person> RemovedOnes = new List<Person>();

        public UnitOfWork()
        {
            IDocumentDbInitializer init = new DocumentDbInitializer();
            string endpointUrl = ConfigurationManager.AppSettings["azure.documentdb.endpointUrl"];
            //string endpointUrl = "https://localhost:8081";
            string authorizationKey = ConfigurationManager.AppSettings["azure.documentdb.authorizationKey"];

            //Get the DB client
            Client = init.GetClient(endpointUrl, authorizationKey);
            string databaseId = ConfigurationManager.AppSettings["azure.documentdb.databaseId"];
            _repo = new DocumentDbRepository<Person>(Client, databaseId);
            //_repo = repo;
            this.WriteToConsoleAndPromptToContinue("Added repo to UOW {0}", "hello");

        }

        public void AddPerson(Person input)
        {
            NewOnes.Add(input);
            this.WriteToConsoleAndPromptToContinue("Added {0} to queue", input.Name.FirstName);
        }

        public async Task<Person> ReadPerson(Person input)
        {
            return await _repo.GetByIdAsync(input.Id);
            //this.WriteToConsoleAndPromptToContinue("added {0} to Readqueue ", input.Id);
        }

        public void ChangePerson(Person input)
        {
            UpdatedOnes.Add(input);
            this.WriteToConsoleAndPromptToContinue("added {0} to change queue", input.Name.FirstName);
        }

        public void DeletePerson(Person input)
        {
            RemovedOnes.Add(input);
            this.WriteToConsoleAndPromptToContinue("Added {0} to Deletequeue", input.Name.FirstName);
        }

        public async Task DoChanges()
        {
            this.WriteToConsoleAndPromptToContinueasd("Do changes to DB{0}", "");
            foreach (var news in NewOnes)
            {
                await _repo.AddOrUpdateAsync(news);
                //this.WriteToConsoleAndPromptToContinue("Added {0} to DB", news.Name);

            }

            foreach (var updates in UpdatedOnes)
            {
                await _repo.AddOrUpdateAsync(updates);
                //this.WriteToConsoleAndPromptToContinue("changed {0} to DB", updates.Name);

            }

            foreach (var removes in RemovedOnes)
            {
                await _repo.RemoveAsync(removes.Id);


                //this.WriteToConsoleAndPromptToContinue("Removed {0} to DB", removes.Name);
            }

            this.WriteToConsoleAndPromptToContinueasd("Did changes to DB{0}", "");

        }

        public IEnumerable<Person> GetAllPersons()
        {
            return _repo.GetAllAsync().Result;
        }
        public Person GetPerson(string id)
        {
            return _repo.GetByIdAsync(id).Result;
        }


        private void WriteToConsoleAndPromptToContinueasd(string format, params object[] args)
        {
            Console.WriteLine(format, args);
            Console.WriteLine("press key to proceed...");
            Console.ReadKey();
        }
        private void WriteToConsoleAndPromptToContinue(string format, params object[] args)
        {
            Console.WriteLine(format, args);
            Console.WriteLine("__________________________________________________");
            //Console.ReadKey();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
