using System;
using System.Collections.Generic;

namespace DAB32.Models
{
    public partial class Addresses
    {
        public Addresses()
        {
            AddressTypes = new HashSet<AddressTypes>();
            PersonAddresses = new HashSet<PersonAddresses>();
        }

        public int Id { get; set; }
        public int CityId { get; set; }
        public int? AddressNameId { get; set; }

        public AddressNames AddressName { get; set; }
        public Cities City { get; set; }
        public ICollection<AddressTypes> AddressTypes { get; set; }
        public ICollection<PersonAddresses> PersonAddresses { get; set; }
    }
}
