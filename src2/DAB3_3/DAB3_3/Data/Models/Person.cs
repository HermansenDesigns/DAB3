using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DAB3_3.Data.Models
{
    public class Person
    {
        [JsonProperty(PropertyName = "id", Required = Required.Always)]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "context", Required = Required.Always)]
        public string Context { get; set; }
        [JsonProperty(PropertyName = "name", Required = Required.Always)]
        public Name Name { get; set; }
        [JsonProperty(PropertyName = "telephoneNumbers", Required = Required.AllowNull)]
        public Telephonenumber[] TelephoneNumbers { get; set; }
        [JsonProperty(PropertyName = "primaryAdress", Required = Required.Always)]
        public Primaryadress PrimaryAdress { get; set; }
        [JsonProperty(PropertyName = "secondaryAdress", Required = Required.AllowNull)]
        public Secondaryadress[] SecondaryAdress { get; set; }
        [JsonProperty(PropertyName = "email", Required = Required.AllowNull)]
        public string Email { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
