using System;
using System.Collections.Generic;

namespace DAB32.Models
{
    public partial class PhoneNumbers
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string Usage { get; set; }
        public string Number { get; set; }
        public int? TelephoneCompanyId { get; set; }

        public Persons Person { get; set; }
        public TelephoneCompanies TelephoneCompany { get; set; }
    }
}
