using Newtonsoft.Json;

namespace Net.Bluewalk.LidlPlus.Models
{
    public class Tax
    {
        [JsonProperty("taxGroup")]
        public long TaxGroup { get; set; }

        [JsonProperty("taxGroupName")]
        public string TaxGroupName { get; set; }

        [JsonProperty("percentage")]
        public decimal Percentage { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("taxableAmount")]
        public decimal TaxableAmount { get; set; }

        [JsonProperty("netAmount")]
        public decimal NetAmount { get; set; }
    }
}