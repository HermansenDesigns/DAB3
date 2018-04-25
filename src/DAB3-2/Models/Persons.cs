using System;
using System.Collections.Generic;

namespace DAB32.Models
{
    public partial class Persons
    {
        public Persons()
        {
            PersonAddressTypes = new HashSet<PersonAddressTypes>();
            PersonAddresses = new HashSet<PersonAddresses>();
            PhoneNumbers = new HashSet<PhoneNumbers>();
        }

        public int Id { get; set; }
        public string Context { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public int? PrimaryAddressId { get; set; }

        public PrimaryAddresses PrimaryAddress { get; set; }
        public ICollection<PersonAddressTypes> PersonAddressTypes { get; set; }
        public ICollection<PersonAddresses> PersonAddresses { get; set; }
        public ICollection<PhoneNumbers> PhoneNumbers { get; set; }
    }
}
