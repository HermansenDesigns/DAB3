using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using DocumentDB.Repository;
using DODB2_2N.Model;
using Microsoft.Azure.Documents.Client.TransientFaultHandling;

/// <summary>
/// In the following sollution we have followed the tutorial:
/// https://docs.microsoft.com/da-dk/azure/cosmos-db/sql-api-dotnetcore-get-started
/// and installed the package:
/// DocumentDB.Repository
/// </summary>
namespace DODB2_2N
{
    class Program
    {
        //public static IReliableReadWriteDocumentClient Client { get; set; }
        static void Main(string[] args)
        {
            //IDocumentDbInitializer init = new DocumentDbInitializer();
            //string endpointUrl = ConfigurationManager.AppSettings["azure.documentdb.endpointUrl"];
            //string authorizationKey = ConfigurationManager.AppSettings["azure.documentdb.authorizationKey"];

            //Get the DB client
            //Client = init.GetClient(endpointUrl, authorizationKey);
            Task t = MainAsync(args);
            t.Wait();
        }
        private static async Task MainAsync(string[] args)
        {
            //string databaseId = ConfigurationManager.AppSettings["azure.documentdb.databaseId"];

            // create repository for persons
            //DocumentDbRepository<Person> repo = new DocumentDbRepository<Person>(Client, databaseId);
            //create instance of Unit Of Work
            UnitOfWork UOW = new UnitOfWork();
            // create a new person
            Person person1 = new Person
            {
                Id = "007",
                Name = new Name{ FirstName = "Ib", MiddleName = null, LastName = "L.Ib" },
                Context = "Boss",
                PrimaryAdress = new Primaryadress
                {
                    AdressName = new AdressName { StreetName = "søndergade", HouseNumber = "12B" },
                    City = new City { Name = "Skandeborg", CityCode = "8660", CountryCode = "DK" } 
                },
                SecondaryAdress = new Secondaryadress[]
                {
                    new Secondaryadress
                    {
                        AdressName=new AdressName{StreetName = "Østervej",HouseNumber = "14"},
                        AdressType = "Work",
                        City = new City {Name = "Stilling", CityCode = "8660", CountryCode = "DK"}
                    },
                    new Secondaryadress
                    {
                        AdressName=new AdressName{StreetName = "Vestergade",HouseNumber = "12A"},
                        AdressType = "Holiday",
                        City = new City {Name = "Sønderborg", CityCode = "6400", CountryCode = "DK"}
                    },
                },
                TelephoneNumbers = new Telephonenumber[]
                {
                    new Telephonenumber {Number = "12345678", Provider = "TDC"},
                    new Telephonenumber {Number = "87654321", Provider = "Telenor"}
                }

            };

            // add person to database's collection (if collection doesn't exist it will be created and named as class name -it's a convenction, that can be configured during initialization of the repository)
            UOW.AddPerson(person1);

            // create another person
            Person person2 = new Person
            {
                Id = "008",
                Name = new Name{ FirstName = "bob", MiddleName = null, LastName = "L.bob" },
                Context = "neighbour",
                PrimaryAdress = new Primaryadress
                {
                    AdressName = new AdressName{ StreetName = "søndergade", HouseNumber = "12A" },
                    City = new City { Name = "Aarhus N", CityCode = "8200", CountryCode = "DK" } 
                },
                SecondaryAdress = new Secondaryadress[]
                {
                    new Secondaryadress
                    {
                        AdressName=new AdressName{StreetName = "ØsterGade",HouseNumber = "10A"},
                        AdressType = "Work",
                        City = new City {Name = "Aarhus V", CityCode = "8210", CountryCode = "DK"}
                    },
                    new Secondaryadress
                    {
                        AdressName=new AdressName{StreetName = "Ågade",HouseNumber = "10"},
                        AdressType = "Holiday",
                        City = new City {Name = "Aarhus C", CityCode = "8000", CountryCode = "DK"}
                    },
                },
                TelephoneNumbers = new Telephonenumber[]
                {
                    new Telephonenumber {Number = "123456789", Provider = "TDC"},
                    new Telephonenumber {Number = "876543219", Provider = "Telenor"}
                }

            };

            // add jack to collection

            UOW.AddPerson(person2);
            await UOW.DoChanges();
            //System.Threading.Thread.Sleep(20000);
            //Person justMatt = await UOW.ReadPerson(person1);
            //Console.WriteLine(justMatt);


            // update name
            person1.Name.FirstName = "matt";
            person1.Name.MiddleName = "matt.M";
            person1.Name.LastName = "matt.L";
            // should update person
            UOW.ChangePerson(person1);
            await UOW.DoChanges();

            // get Matt by his Id
            //justMatt = await UOW.ReadPerson(person1);
            // Console.WriteLine(justMatt);


            // remove matt from collection
            //UOW.DeletePerson(person1);
           // await UOW.DoChanges();


        }
    }

}
