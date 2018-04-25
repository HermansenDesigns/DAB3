using System;
using System.Collections.Generic;

namespace DAB32.Models
{
    public partial class Cities
    {
        public Cities()
        {
            Addresses = new HashSet<Addresses>();
            PrimaryAddresses = new HashSet<PrimaryAddresses>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string ZipCode { get; set; }
        public int CountryCodeId { get; set; }

        public CountryCodes CountryCode { get; set; }
        public ICollection<Addresses> Addresses { get; set; }
        public ICollection<PrimaryAddresses> PrimaryAddresses { get; set; }
    }
}
