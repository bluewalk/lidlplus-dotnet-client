using Newtonsoft.Json;

namespace Net.Bluewalk.LidlPlus.Models
{
    public class Store
    {
        [JsonProperty("storeKey")]
        public string StoreKey { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("schedule")]
        public string Schedule { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("postalCode")]
        public string PostalCode { get; set; }

        [JsonProperty("locality")]
        public string Locality { get; set; }

        [JsonProperty("isLidlPlus")]
        public bool IsLidlPlus { get; set; }

        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("hasParkingForDisabled")]
        public bool HasParkingForDisabled { get; set; }

        [JsonProperty("hasParking")]
        public bool HasParking { get; set; }

        [JsonProperty("hasBackery")]
        public bool HasBackery { get; set; }

        [JsonProperty("hasPackage")]
        public bool HasPackage { get; set; }

        [JsonProperty("province")]
        public string Province { get; set; }

        [JsonProperty("countryZone")]
        public object CountryZone { get; set; }
    }
}
