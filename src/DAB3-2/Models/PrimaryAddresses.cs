using System;
using System.Collections.Generic;

namespace DAB32.Models
{
    public partial class PrimaryAddresses
    {
        public PrimaryAddresses()
        {
            Persons = new HashSet<Persons>();
        }

        public int Id { get; set; }
        public int? AddressNameId { get; set; }
        public int CityId { get; set; }

        public AddressNames AddressName { get; set; }
        public Cities City { get; set; }
        public ICollection<Persons> Persons { get; set; }
    }
}
