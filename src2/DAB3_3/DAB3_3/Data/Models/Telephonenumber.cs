using Newtonsoft.Json;

namespace DAB3_3.Data.Models
{
    public class Telephonenumber
    {
        [JsonProperty(PropertyName = "number", Required = Required.Always)]
        public string Number { get; set; }
        [JsonProperty(PropertyName = "provider", Required = Required.Always)]
        public string Provider { get; set; }
    }
}