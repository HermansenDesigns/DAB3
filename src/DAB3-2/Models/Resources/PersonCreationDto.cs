using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAB32.Models.Resources
{
    public class PersonCreationDto
    {
        public string Name { get; set; }
        public string Context { get; set; }
        public string Email { get; set; }
        public List<PhoneNumberCreationDto> PhoneNumberDtos { get; set; }
        public PrimaryAddressCreationDto PrimaryAddressDto { get; set; }
        public List<SecondaryAddressCreationDto> SecondaryAddressDtos { get; set; }
    }
}
