using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CurrencyTest.Models.Fixer
{

    public class FixerApiResult
    {
        [JsonProperty(PropertyName = "base")]
        public string Base { get; set; }
        [JsonProperty(PropertyName = "date")]
        public DateTime Date { get; set; }
        [JsonProperty(PropertyName = "rates")]
        public Rates Rates { get; set; }
    }

    public class Rates
    {
        public float AUD { get; set; }
        public float BGN { get; set; }
        public float BRL { get; set; }
        public float CAD { get; set; }
        public float CHF { get; set; }
        public float CNY { get; set; }
        public float CZK { get; set; }
        public float DKK { get; set; }
        public float GBP { get; set; }
        public float HKD { get; set; }
        public float HRK { get; set; }
        public float HUF { get; set; }
        public float IDR { get; set; }
        public float ILS { get; set; }
        public float INR { get; set; }
        public float JPY { get; set; }
        public float KRW { get; set; }
        public float MXN { get; set; }
        public float MYR { get; set; }
        public float NOK { get; set; }
        public float NZD { get; set; }
        public float PHP { get; set; }
        public float PLN { get; set; }
        public float RON { get; set; }
        public float RUB { get; set; }
        public float SEK { get; set; }
        public float SGD { get; set; }
        public float THB { get; set; }
        public float TRY { get; set; }
        public float ZAR { get; set; }
        public float EUR { get; set; }
        public float USD { get; set; }
    }

    public static class FixerApiResultExtenssions
    {
        public static IDictionary<string, float> RatesToDictionary(this FixerApiResult param)
        {
            var returnDict = new Dictionary<string, float>();
            returnDict.Add("AUD", param.Rates.AUD);
            returnDict.Add("BGN", param.Rates.BGN);
            returnDict.Add("BRL", param.Rates.BRL);
            returnDict.Add("CAD", param.Rates.CAD);
            returnDict.Add("CHF", param.Rates.CHF);
            returnDict.Add("CNY", param.Rates.CNY);
            returnDict.Add("CZK", param.Rates.CZK);
            returnDict.Add("DKK", param.Rates.DKK);
            returnDict.Add("GBP", param.Rates.GBP);
            returnDict.Add("HKD", param.Rates.HKD);
            returnDict.Add("HRK", param.Rates.HRK);
            returnDict.Add("HUF", param.Rates.HUF);
            returnDict.Add("IDR", param.Rates.IDR);
            returnDict.Add("ILS", param.Rates.ILS);
            returnDict.Add("INR", param.Rates.INR);
            returnDict.Add("JPY", param.Rates.JPY);
            returnDict.Add("KRW", param.Rates.KRW);
            returnDict.Add("MXN", param.Rates.MXN);
            returnDict.Add("MYR", param.Rates.MYR);
            returnDict.Add("NOK", param.Rates.NOK);
            returnDict.Add("NZD", param.Rates.NZD);
            returnDict.Add("PHP", param.Rates.PHP);
            returnDict.Add("PLN", param.Rates.PLN);
            returnDict.Add("RON", param.Rates.RON);
            returnDict.Add("RUB", param.Rates.RUB);
            returnDict.Add("SEK", param.Rates.SEK);
            returnDict.Add("SGD", param.Rates.SGD);
            returnDict.Add("THB", param.Rates.THB);
            returnDict.Add("TRY", param.Rates.TRY);
            returnDict.Add("ZAR", param.Rates.ZAR);
            returnDict.Add("EUR", param.Rates.EUR);
            returnDict.Add("USD", param.Rates.USD);
            return returnDict;
        }

        public static float GetRateForCurrencies(this FixerApiResult param, string firstCurrency, string secondCurrency)
        {
            var resultDictionary = param.RatesToDictionary();

            float currency;

            if (firstCurrency.Equals(secondCurrency))
            {
                currency = 1f;
            }
            else if (param.Base.Equals(firstCurrency))
            {
                currency = 1 / resultDictionary[secondCurrency];
            }
            else if (param.Base.Equals(secondCurrency))
            {
                currency = resultDictionary[firstCurrency];
            }
            else
            {
                float firstCurrencyRate = resultDictionary[firstCurrency];
                float secondCurrencyRate = resultDictionary[secondCurrency];
                currency = firstCurrencyRate / secondCurrencyRate;
            }

            return currency;
        }
    }
}
