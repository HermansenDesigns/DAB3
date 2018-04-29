using Newtonsoft.Json;

namespace DAB3_3.Lib.Model
{

    public class Person
    {
        [JsonProperty(PropertyName = "id",Required = Required.Always)]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "context", Required = Required.Always)]
        public string Context { get; set; }
        [JsonProperty(PropertyName = "name", Required = Required.Always)]
        public Name Name { get; set; }
        [JsonProperty(PropertyName = "telephoneNumbers", Required = Required.AllowNull)]
        public Telephonenumber[] TelephoneNumbers { get; set; }
        [JsonProperty(PropertyName = "primaryAdress",Required = Required.Always)]
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

    public class Name
    {
        [JsonProperty(PropertyName = "firstName", Required = Required.Always)]
        public string FirstName { get; set; }
        [JsonProperty(PropertyName = "middleName", Required = Required.AllowNull)]
        public string MiddleName { get; set; }
        [JsonProperty(PropertyName = "lastName", Required = Required.Always)]
        public string LastName { get; set; }

    }

    public class Primaryadress
    {
        [JsonProperty(PropertyName = "adressName", Required = Required.Always)]
        public AdressName AdressName { get; set; }
        [JsonProperty(PropertyName = "city", Required = Required.Always)]
        public City City { get; set; }
    }

    public class AdressName
    {
        [JsonProperty(PropertyName = "streetName", Required = Required.Always)]
        public string StreetName { get; set; }
        [JsonProperty(PropertyName = "houseNumber", Required = Required.Always)]
        public string HouseNumber { get; set; }
    }

    public class Telephonenumber
    {
        [JsonProperty(PropertyName = "number", Required = Required.Always)]
        public string Number { get; set; }
        [JsonProperty(PropertyName = "provider", Required = Required.Always)]
        public string Provider { get; set; }
    }

    public class Secondaryadress
    {
        [JsonProperty(PropertyName = "adressName", Required = Required.Always)]
        public AdressName AdressName { get; set; }
        [JsonProperty(PropertyName = "adressType", Required = Required.AllowNull)]
        public string AdressType { get; set; }
        [JsonProperty(PropertyName = "city", Required = Required.Always)]
        public City City { get; set; }
    }

    public class City
    {
        [JsonProperty(PropertyName = "name", Required = Required.Always)]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "cityCode",Required = Required.Always)]
        public string CityCode { get; set; }
        [JsonProperty(PropertyName = "countryCode", Required = Required.Always)]
        public string CountryCode { get; set; }
    }

}