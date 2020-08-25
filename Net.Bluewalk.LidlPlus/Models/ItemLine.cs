using System.Collections.Generic;
using Newtonsoft.Json;

namespace Net.Bluewalk.LidlPlus.Models
{
    public class ItemLine
    {
        [JsonProperty("currentUnitPrice")]
        public decimal CurrentUnitPrice { get; set; }

        [JsonProperty("quantity")]
        public decimal Quantity { get; set; }

        [JsonProperty("isWeight")]
        public bool IsWeight { get; set; }

        [JsonProperty("originalAmount")]
        public decimal OriginalAmount { get; set; }

        [JsonProperty("extendedAmount")]
        public decimal ExtendedAmount { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("taxGroup")]
        public long TaxGroup { get; set; }

        [JsonProperty("taxGroupName")]
        public string TaxGroupName { get; set; }

        [JsonProperty("codeInput")]
        public string CodeInput { get; set; }

        [JsonProperty("discounts")]
        public List<Discount> Discounts { get; set; }

        [JsonProperty("deposit")]
        public Deposit Deposit { get; set; }

        [JsonProperty("giftSerialNumber")]
        public object GiftSerialNumber { get; set; }
    }
}