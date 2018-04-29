using Newtonsoft.Json;

namespace DAB3_3.Data.Models
{
    public class Primaryadress
    {
        [JsonProperty(PropertyName = "adressName", Required = Required.Always)]
        public AdressName AdressName { get; set; }
        [JsonProperty(PropertyName = "city", Required = Required.Always)]
        public City City { get; set; }
    }
}