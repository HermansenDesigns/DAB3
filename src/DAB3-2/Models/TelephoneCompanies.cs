using System;
using System.Collections.Generic;

namespace DAB32.Models
{
    public partial class TelephoneCompanies
    {
        public TelephoneCompanies()
        {
            PhoneNumbers = new HashSet<PhoneNumbers>();
        }

        public int Id { get; set; }
        public string CompanyName { get; set; }

        public ICollection<PhoneNumbers> PhoneNumbers { get; set; }
    }
}
