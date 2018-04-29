using Newtonsoft.Json;

namespace DAB3_3.Data.Models
{
    public class City
    {
        [JsonProperty(PropertyName = "name", Required = Required.Always)]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "cityCode", Required = Required.Always)]
        public string CityCode { get; set; }
        [JsonProperty(PropertyName = "countryCode", Required = Required.Always)]
        public string CountryCode { get; set; }
    }
}