using System;
using System.Collections.Generic;

namespace DAB32.Models
{
    public partial class AddressTypes
    {
        public AddressTypes()
        {
            PersonAddressTypes = new HashSet<PersonAddressTypes>();
        }

        public int Id { get; set; }
        public int? AddressId { get; set; }
        public string Type { get; set; }

        public Addresses Address { get; set; }
        public ICollection<PersonAddressTypes> PersonAddressTypes { get; set; }
    }
}
