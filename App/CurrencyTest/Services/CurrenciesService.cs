using System;
using System.Linq;
using System.Threading.Tasks;
using CurrencyTest.Infrastructure.Interfaces;
using CurrencyTest.Infrastructure.Models;

namespace CurrencyTest.Services
{
    public class CurrenciesService : ICurrenciesService
    {
        private readonly ICurrenciesConfiguration _configuration;
        private readonly ICurrencyApi _apiProvider;

        public CurrenciesService(ICurrencyApi apiProvider, ICurrenciesConfiguration configuration)
        {
            this._configuration = configuration;
            this._apiProvider = apiProvider;
        }

        public async Task<CurrencyPair> GetCurrentCurrencyRate(string firstCurrency, string secondCurrency)
        {
            CheckCurrences(firstCurrency, secondCurrency);
            return await _apiProvider.GetCurentCurrencyRate(firstCurrency, secondCurrency);
        }

        public async Task<CurrencyPair> GetCurrencyRateByDate(string firstCurrency, string secondCurrency, DateTime date)
        {
            CheckCurrences(firstCurrency, secondCurrency);
            return await _apiProvider.GetCurrencyRateByDate(firstCurrency, secondCurrency, date);
        }

        public async Task<CurrencyPair> GetAverageCurencyRateForDates(string firstCurrency, string secondCurrency,
            DateTime startDate, DateTime endDate)
        {
            CheckCurrences(firstCurrency, secondCurrency);
            var resultsCollection = await _apiProvider.GetCurrencyByDateRange(firstCurrency, secondCurrency, startDate, endDate);

            float average = resultsCollection.Select(x => x.ExchangeRate).Average();

            return new CurrencyPair(firstCurrency, secondCurrency, average, startDate, endDate);
        }

        public async Task<CurrencyPair> GetMaximumCurencyRateForDates(string firstCurrency, string secondCurrency, DateTime startDate, DateTime endDate)
        {
            CheckCurrences(firstCurrency, secondCurrency);
            var resultsCollection = await _apiProvider.GetCurrencyByDateRange(firstCurrency, secondCurrency, startDate, endDate);

            float maximum = resultsCollection.Select(x => x.ExchangeRate).Max();

            return new CurrencyPair(firstCurrency, secondCurrency, maximum, startDate, endDate);
        }

        public async Task<CurrencyPair> GetMinimumCurencyRateForDates(string firstCurrency, string secondCurrency, DateTime startDate, DateTime endDate)
        {
            CheckCurrences(firstCurrency, secondCurrency);
            var resultsCollection = await _apiProvider.GetCurrencyByDateRange(firstCurrency, secondCurrency, startDate, endDate);

            float minimum = resultsCollection.Select(x => x.ExchangeRate).Min();

            return new CurrencyPair(firstCurrency, secondCurrency, minimum, startDate, endDate);
        }

        private void CheckCurrences(params string[] currencies)
        {
            foreach (var currency in currencies)
            {
                if (!_configuration.AvaibleCurrencies.Contains(currency))
                {
                    throw new InvalidOperationException(string.Format("Cannot use currency {0}", currency));
                }
            }
        }
    }
}
