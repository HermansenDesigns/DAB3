namespace DAB32.Models.Resources
{
    public class PrimaryAddressUpdateDto
    {
        public int Id { get; set; }
        public int AddressNameId { get; set; }
        public string Name { get; set; }
        public AddressUpdateDto AddressDto { get; set; }
    }
}