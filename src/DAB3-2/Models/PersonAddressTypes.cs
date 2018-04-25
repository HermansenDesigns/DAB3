using System;
using System.Collections.Generic;

namespace DAB32.Models
{
    public partial class PersonAddressTypes
    {
        public int PersonId { get; set; }
        public int AddressTypeId { get; set; }

        public AddressTypes AddressType { get; set; }
        public Persons Person { get; set; }
    }
}
