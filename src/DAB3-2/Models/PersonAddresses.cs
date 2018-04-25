using System;
using System.Collections.Generic;

namespace DAB32.Models
{
    public partial class PersonAddresses
    {
        public int PersonId { get; set; }
        public int AddressId { get; set; }

        public Addresses Address { get; set; }
        public Persons Person { get; set; }
    }
}
