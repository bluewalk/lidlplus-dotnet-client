using Newtonsoft.Json;

namespace Net.Bluewalk.LidlPlus.Models
{
    public class Location
    {
        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }
    }
}