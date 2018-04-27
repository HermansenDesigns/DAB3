namespace DAB32.Models.Resources
{
    public class SecondaryAddressCreationDto
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public AddressCreationDto AddressDto { get; set; }
    }
}