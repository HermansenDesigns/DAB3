namespace DAB32.Models.Resources
{
    public class AddressUpdateDto
    {
        public int AddressId { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string Zip { get; set; }
        public string StreetName { get; set; }
        public string Housenumber { get; set; }
        public int CountryCodeId { get; set; }
        public string CountryCode { get; set; }
    }
}