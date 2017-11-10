using System;
using CurrencyTest.Infrastructure.Interfaces;

namespace CurrencyTest.Configurations
{
    public class CurrenciesConfiguration : ICurrenciesConfiguration
    {
        public string[] AvaibleCurrencies { get; }

        public CurrenciesConfiguration()
        {
            var values = Environment.GetEnvironmentVariable("AVAIBLE_CURRENCIES");
            AvaibleCurrencies = values.Split(',');
        }
    }
}
