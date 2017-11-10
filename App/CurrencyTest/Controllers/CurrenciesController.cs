using System;
using System.Threading.Tasks;
using CurrencyTest.Infrastructure.Interfaces;
using CurrencyTest.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyTest.Controllers
{
    [Route("api/[controller]/[action]")]
    public class CurrenciesController : Controller
    {
        private readonly ICurrenciesService _currencyService;

        public CurrenciesController(ICurrenciesService currencyService)
        {
            this._currencyService = currencyService;
        }

        [HttpGet("{firstCurrency},{secondCurrency}")]
        public async Task<CurrencyPair> GetCurrentCurrencyRate(string firstCurrency, string secondCurrency)
        {
            return await _currencyService.GetCurrentCurrencyRate(firstCurrency, secondCurrency);
        }

        [HttpGet("{firstCurrency},{secondCurrency},{date}")]
        public async Task<CurrencyPair> GetCurrencyRateByDate(string firstCurrency, string secondCurrency, DateTime date)
        {
            return await _currencyService.GetCurrencyRateByDate(firstCurrency, secondCurrency, date);
        }

        [HttpGet("{firstCurrency},{secondCurrency},{startDate},{endDate}")]
        public async Task<CurrencyPair> GetAverageCurrencyRateByDate(string firstCurrency, string secondCurrency, DateTime startDate, DateTime endDate)
        {
            return await _currencyService.GetAverageCurencyRateForDates(firstCurrency, secondCurrency, startDate, endDate);
        }

        [HttpGet("{firstCurrency},{secondCurrency},{startDate},{endDate}")]
        public async Task<CurrencyPair> GetMaximumCurrencyRateByDate(string firstCurrency, string secondCurrency, DateTime startDate, DateTime endDate)
        {
            return await _currencyService.GetMaximumCurencyRateForDates(firstCurrency, secondCurrency, startDate, endDate);
        }

        [HttpGet("{firstCurrency},{secondCurrency},{startDate},{endDate}")]
        public async Task<CurrencyPair> GetMinimumCurrencyRateByDate(string firstCurrency, string secondCurrency, DateTime startDate, DateTime endDate)
        {
            return await _currencyService.GetMinimumCurencyRateForDates(firstCurrency, secondCurrency, startDate, endDate);
        }
    }
}
