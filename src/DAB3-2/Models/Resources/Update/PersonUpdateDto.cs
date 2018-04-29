using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAB32.Models.Resources
{
    public class PersonUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Context { get; set; }
        public string Email { get; set; }
        public List<PhoneNumberUpdateDto> PhoneNumberDtos { get; set; }
        public PrimaryAddressUpdateDto PrimaryAddressDto { get; set; }
        public List<SecondaryAddressUpdateDto> SecondaryAddressDtos { get; set; }
    }
}
