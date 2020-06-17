using Newtonsoft.Json;

namespace Net.Bluewalk.LidlPlus.Models
{
    public class Currency
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }
    }
}