using System;
using System.Threading.Tasks;
using CurrencyTest.Infrastructure.Models;

namespace CurrencyTest.Infrastructure.Interfaces
{
    public interface ICurrenciesService
    {
        Task<CurrencyPair> GetCurrentCurrencyRate(string firstCurrency, string secondCurrency);
        Task<CurrencyPair> GetCurrencyRateByDate(string firstCurrency, string secondCurrency, DateTime date);
        Task<CurrencyPair> GetAverageCurencyRateForDates(string firstCurrency, string secondCurrency,
            DateTime startDate, DateTime endDate);
        Task<CurrencyPair> GetMaximumCurencyRateForDates(string firstCurrency, string secondCurrency, DateTime startDate, DateTime endDate);
        Task<CurrencyPair> GetMinimumCurencyRateForDates(string firstCurrency, string secondCurrency, DateTime startDate, DateTime endDate);
    }
}
