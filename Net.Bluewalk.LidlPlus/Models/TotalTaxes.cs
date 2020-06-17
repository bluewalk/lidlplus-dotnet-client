
using Newtonsoft.Json;

namespace Net.Bluewalk.LidlPlus.Models
{
    public class TotalTaxes
    {
        [JsonProperty("totalAmount")]
        public decimal TotalAmount { get; set; }

        [JsonProperty("totalTaxableAmount")]
        public decimal TotalTaxableAmount { get; set; }

        [JsonProperty("totalNetAmount")]
        public decimal TotalNetAmount { get; set; }
    }
}