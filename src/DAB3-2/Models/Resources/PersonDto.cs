using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAB32.Models.Resources
{
    public class PersonDto
    {
        public int Id { get; set; }
        public string Context { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }

        public int? PrimaryAddressId { get; set; }
        public PrimaryAddressCreationDto PrimaryAddressCreation { get; set; }
    }
}
