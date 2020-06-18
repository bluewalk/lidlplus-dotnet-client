using Newtonsoft.Json;

namespace Net.Bluewalk.LidlPlus.Models
{
    public class Deposit
    {
        [JsonProperty("quantity")]
        public long Quantity { get; set; }

        [JsonProperty("taxGroup")]
        public long TaxGroup { get; set; }

        [JsonProperty("taxGroupName")]
        public string TaxGroupName { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("unitPrice")]
        public decimal UnitPrice { get; set; }
    }
}