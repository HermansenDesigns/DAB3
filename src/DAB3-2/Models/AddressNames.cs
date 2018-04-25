using System;
using System.Collections.Generic;

namespace DAB32.Models
{
    public partial class AddressNames
    {
        public AddressNames()
        {
            Addresses = new HashSet<Addresses>();
            PrimaryAddresses = new HashSet<PrimaryAddresses>();
        }

        public int Id { get; set; }
        public string HouseNumber { get; set; }
        public string StreetName { get; set; }

        public ICollection<Addresses> Addresses { get; set; }
        public ICollection<PrimaryAddresses> PrimaryAddresses { get; set; }
    }
}
