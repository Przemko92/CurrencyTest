using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CurrencyTest.Configurations;
using CurrencyTest.Infrastructure.Helpers;
using CurrencyTest.Infrastructure.Interfaces;
using CurrencyTest.Infrastructure.Models;
using CurrencyTest.Models.Fixer;
using Newtonsoft.Json;

namespace CurrencyTest.Providers
{
    public class FixerApiProvider : ICurrencyApi
    {
        private readonly HttpClient _client;
        private readonly FixerConfiguration _configuration;

        public FixerApiProvider(FixerConfiguration configuration)
        {
            this._configuration = configuration;
            this._client = new HttpClient();
        }

        public void Initilize()
        {
            this._client.BaseAddress = new Uri(this._configuration.BaseUri);
        }

        public async Task<CurrencyPair> GetCurentCurrencyRate(string firstCurrency, string secondCurrency)
        {
            CheckInitialization();
            CheckCurrences(firstCurrency, secondCurrency);

            firstCurrency = firstCurrency.ToUpper();
            secondCurrency = secondCurrency.ToUpper();

            var result = await ReadResponse<FixerApiResult>(
                await _client.GetAsync(string.Format("latest?symbols={0},{1}", firstCurrency, secondCurrency)));

            float rate = result.GetRateForCurrencies(firstCurrency, secondCurrency);

            return new CurrencyPair(firstCurrency, secondCurrency, rate, result.Date, result.Date);
        }

        public async Task<CurrencyPair> GetCurrencyRateByDate(string firstCurrency, string secondCurrency, DateTime date)
        {
            CheckInitialization();
            CheckCurrences(firstCurrency, secondCurrency);
            if (date < _configuration.MinimumDateRange)
            {
                throw new InvalidOperationException("Minimum date is " + _configuration.MinimumDateRange);
            }

            firstCurrency = firstCurrency.ToUpper();
            secondCurrency = secondCurrency.ToUpper();

            var result = await ReadResponse<FixerApiResult>(
                await _client.GetAsync(string.Format("{0}", date.ToString("yyyy-MM-dd"))));

            float rate = result.GetRateForCurrencies(firstCurrency, secondCurrency);

            return new CurrencyPair(firstCurrency, secondCurrency, rate, result.Date, result.Date);
        }

        public async Task<IEnumerable<CurrencyPair>> GetCurrencyByDateRange(string firstCurrency, string secondCurrency, DateTime startDate, DateTime endDate)
        {
            CheckInitialization();
            CheckCurrences(firstCurrency, secondCurrency);
            if (startDate > endDate)
            {
                throw new InvalidOperationException("StartDate cannot be greater than EndDate");
            }

            if (startDate < _configuration.MinimumDateRange)
            {
                throw new InvalidOperationException("Minimum date is " + _configuration.MinimumDateRange);
            }


            firstCurrency = firstCurrency.ToUpper();
            secondCurrency = secondCurrency.ToUpper();
            ICollection<CurrencyPair> result = new Collection<CurrencyPair>();

            foreach (var date in DateTimeHelper.GetDatesCollection(startDate, endDate))
            {
                var apiResult = await ReadResponse<FixerApiResult>(
                   await _client.GetAsync(string.Format("{0}", date.ToString("yyyy-MM-dd"))));

                float rate = apiResult.GetRateForCurrencies(firstCurrency, secondCurrency);
                result.Add(new CurrencyPair(firstCurrency, secondCurrency, rate, apiResult.Date, apiResult.Date));
            }

            return result;
        }

        private void CheckCurrences(params string[] currencies)
        {
            if (currencies.Any(x => !_configuration.SupportedCurrencies.Contains(x)))
            {
                throw new InvalidOperationException();
            }
        }

        private void CheckInitialization()
        {
            if (_client.BaseAddress == null)
            {
                throw new InvalidOperationException("Provider not initialized");
            }
        }

        private async Task<T> ReadResponse<T>(HttpResponseMessage responseMessage)
        {
            var result = await responseMessage.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(result);
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
