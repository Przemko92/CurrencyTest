using System;

namespace CurrencyTest.Configurations
{
    public class FixerConfiguration
    {
        public string BaseUri { get; set; }
        public string[] SupportedCurrencies { get; set; }
        public DateTime MinimumDateRange { get; set; }
    }
}
