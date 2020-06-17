using Newtonsoft.Json;

namespace Net.Bluewalk.LidlPlus.Models
{
    public class ForeignPayment
    {
        [JsonProperty("foreignAmount")]
        public decimal ForeignAmount { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("foreignCurrency")]
        public string ForeignCurrency { get; set; }
    }
}