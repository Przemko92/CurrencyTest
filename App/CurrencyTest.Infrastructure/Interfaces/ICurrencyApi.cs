using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CurrencyTest.Infrastructure.Models;

namespace CurrencyTest.Infrastructure.Interfaces
{
    public interface ICurrencyApi : IDisposable
    {
        Task<CurrencyPair> GetCurentCurrencyRate(string firstCurrency, string secondCurrency);
        Task<CurrencyPair> GetCurrencyRateByDate(string firstCurrency, string secondCurrency, DateTime date);

        Task<IEnumerable<CurrencyPair>> GetCurrencyByDateRange(string firstCurrency, string secondCurrency,
            DateTime startDate, DateTime endDate);
    }
}
