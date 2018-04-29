using Newtonsoft.Json;

namespace DAB3_3.Data.Models
{
    public class Name
    {
        [JsonProperty(PropertyName = "firstName", Required = Required.Always)]
        public string FirstName { get; set; }
        [JsonProperty(PropertyName = "middleName", Required = Required.AllowNull)]
        public string MiddleName { get; set; }
        [JsonProperty(PropertyName = "lastName", Required = Required.Always)]
        public string LastName { get; set; }

    }
}