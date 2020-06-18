using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Net.Bluewalk.LidlPlus.Models
{
    public partial class Ticket
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("barCode")]
        public string BarCode { get; set; }

        [JsonProperty("sequenceNumber")]
        public long SequenceNumber { get; set; }

        [JsonProperty("workstation")]
        public string Workstation { get; set; }

        [JsonProperty("itemsLine")]
        public List<ItemLine> ItemsLine { get; set; }

        [JsonProperty("taxes")]
        public List<Tax> Taxes { get; set; }

        [JsonProperty("totalTaxes")]
        public TotalTaxes TotalTaxes { get; set; }

        [JsonProperty("couponsUsed")]
        public List<object> CouponsUsed { get; set; }

        [JsonProperty("returnedTickets")]
        public List<object> ReturnedTickets { get; set; }

        [JsonProperty("isFavorite")]
        public bool IsFavorite { get; set; }

        [JsonProperty("date")]
        public DateTimeOffset Date { get; set; }

        [JsonProperty("totalAmount")]
        public decimal TotalAmount { get; set; }

        [JsonProperty("sumAmount")]
        public decimal SumAmount { get; set; }

        [JsonProperty("storeCode")]
        public string StoreCode { get; set; }

        [JsonProperty("currency")]
        public Currency Currency { get; set; }

        [JsonProperty("payments")]
        public List<Payment> Payments { get; set; }

        [JsonProperty("tenderChange")]
        public List<object> TenderChange { get; set; }

        [JsonProperty("fiscalDataAt")]
        public object FiscalDataAt { get; set; }

        [JsonProperty("isEmployee")]
        public bool IsEmployee { get; set; }

        [JsonProperty("linesScannedCount")]
        public decimal LinesScannedCount { get; set; }

        [JsonProperty("totalDiscount")]
        public decimal TotalDiscount { get; set; }

        [JsonProperty("taxExcemptTexts")]
        public string TaxExcemptTexts { get; set; }

        [JsonProperty("invoiceRequestId")]
        public object InvoiceRequestId { get; set; }

        [JsonProperty("invoiceId")]
        public object InvoiceId { get; set; }

        [JsonProperty("ustIdNr")]
        public object UstIdNr { get; set; }

        [JsonProperty("languageCode")]
        public string LanguageCode { get; set; }

        [JsonProperty("fiscalDataCompanyTax")]
        public object FiscalDataCompanyTax { get; set; }

        [JsonProperty("fiscalDataStoreTax")]
        public object FiscalDataStoreTax { get; set; }

        [JsonProperty("fiscalDataBkp")]
        public object FiscalDataBkp { get; set; }

        [JsonProperty("fiscalDataPkp")]
        public object FiscalDataPkp { get; set; }

        [JsonProperty("fiscalDataRin")]
        public object FiscalDataRin { get; set; }

        [JsonProperty("fiscalDataSalesRegime")]
        public object FiscalDataSalesRegime { get; set; }

        [JsonProperty("fiscalDataFik")]
        public object FiscalDataFik { get; set; }

        [JsonProperty("operatorId")]
        public long OperatorId { get; set; }
    }
}
