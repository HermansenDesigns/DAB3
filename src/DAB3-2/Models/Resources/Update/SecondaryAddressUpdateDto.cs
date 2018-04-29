namespace DAB32.Models.Resources
{
    public class SecondaryAddressUpdateDto
    {
        public int AddressNameId { get; set; }
        public string Name { get; set; }
        public int AddressTypeId { get; set; }
        public string Type { get; set; }
        public AddressUpdateDto AddressDto { get; set; }
    }
}