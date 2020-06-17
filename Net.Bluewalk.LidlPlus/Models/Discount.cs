using Newtonsoft.Json;

namespace Net.Bluewalk.LidlPlus.Models
{
    public class Discount
    {
        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}