using Newtonsoft.Json;

namespace Net.Bluewalk.LidlPlus.Models
{
    public class CardInfo
    {
        [JsonProperty("accountNumber")]
        public string AccountNumber { get; set; }

        [JsonProperty("authorizationMethodCode")]
        public string AuthorizationMethodCode { get; set; }

        [JsonProperty("transactionNumber")]
        public string TransactionNumber { get; set; }

        [JsonProperty("cardTypeCode")]
        //[JsonConverter(typeof(ParseStringConverter))]
        public long CardTypeCode { get; set; }

        [JsonProperty("transactionTypeCode")]
        public string TransactionTypeCode { get; set; }

        [JsonProperty("terminalId")]
        public string TerminalId { get; set; }

        [JsonProperty("authorizationMiscSettlementData")]
        public string AuthorizationMiscSettlementData { get; set; }
    }
}