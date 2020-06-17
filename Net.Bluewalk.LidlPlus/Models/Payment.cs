using System;
using Newtonsoft.Json;

namespace Net.Bluewalk.LidlPlus.Models
{
    public class Payment
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("roundingDifference")]
        public decimal RoundingDifference { get; set; }

        [JsonProperty("foreignPayment")]
        public ForeignPayment ForeignPayment { get; set; }

        [JsonProperty("cardInfo")]
        public CardInfo CardInfo { get; set; }

        [JsonProperty("beginDate")]
        public DateTimeOffset BeginDate { get; set; }

        [JsonProperty("endDate")]
        public DateTimeOffset EndDate { get; set; }

        [JsonProperty("rawPaymentInformationHTML")]
        public string RawPaymentInformationHtml { get; set; }
    }
}