using System;

namespace CurrencyTest.Infrastructure.Models
{
    public class CurrencyPair
    {
        public string BaseCurrency { get; }
        public string DestinationCurrency { get; }
        public float ExchangeRate { get; }
        public DateTime FromDate { get; }
        public DateTime EndDate { get; }

        public CurrencyPair(string baseCurrency, string destinationCurrency, float exchangeRate, DateTime fromDate, DateTime endDate)
        {
            this.BaseCurrency = baseCurrency;
            this.DestinationCurrency = destinationCurrency;
            this.ExchangeRate = exchangeRate;
            this.FromDate = fromDate;
            this.EndDate = endDate;
        }
    }
}
