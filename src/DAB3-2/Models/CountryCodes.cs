using System;
using System.Collections.Generic;

namespace DAB32.Models
{
    public partial class CountryCodes
    {
        public int Id { get; set; }
        public string Code { get; set; }

        public Cities Cities { get; set; }
    }
}
