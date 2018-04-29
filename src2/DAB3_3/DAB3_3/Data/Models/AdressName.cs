using Newtonsoft.Json;

namespace DAB3_3.Data.Models
{
    public class AdressName
    {
        [JsonProperty(PropertyName = "streetName", Required = Required.Always)]
        public string StreetName { get; set; }
        [JsonProperty(PropertyName = "houseNumber", Required = Required.Always)]
        public string HouseNumber { get; set; }
    }
}