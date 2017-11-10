using System;
using CurrencyTest.Configurations;
using CurrencyTest.Providers;
using Xunit;

namespace CurrencyTest.Tests.Providers
{
    public class FixerApiProviderTests
    {
        [Fact]
        public void GetCurrencyByDateRange_ThrowsInvalidOperationException_WhenStartDateIsGreaterThenEndDate()
        {
            //Arrange
            FixerConfiguration config = new FixerConfiguration()
            {
                BaseUri = "https://api.fixer.io",
                MinimumDateRange = new DateTime(2000, 01, 01),
                SupportedCurrencies = new[] { "USD", "EUR", "PLN" }
            };

            FixerApiProvider provider = new FixerApiProvider(config);
            //Act
            //Assert
            Assert.ThrowsAsync<InvalidOperationException>(() => provider.GetCurrencyByDateRange("PLN", "USD", DateTime.Now.AddDays(1), DateTime.Now));
        }

        [Fact]
        public void GetCurrencyByDateRange_ThrowsInvalidOperationException_WhenWrongDateProvided()
        {
            //Arrange
            FixerConfiguration config = new FixerConfiguration()
            {
                BaseUri = "https://api.fixer.io",
                MinimumDateRange = new DateTime(2000, 01, 01),
                SupportedCurrencies = new[] { "USD", "EUR", "PLN" }
            };

            FixerApiProvider provider = new FixerApiProvider(config);
            //Act
            //Assert
            Assert.ThrowsAsync<InvalidOperationException>(() => provider.GetCurrencyByDateRange("PLN", "USD", new DateTime(1999, 01, 02), DateTime.Now));
        }

        [Fact]
        public void GetCurentCurrencyRate_ThrowsInvalidOperationException_WhenProviderNotInitialized()
        {
            //Arrange
            FixerConfiguration config = new FixerConfiguration()
            {
                BaseUri = "https://api.fixer.io",
                MinimumDateRange = new DateTime(2000, 01, 01),
                SupportedCurrencies = new[] { "USD", "EUR", "PLN" }
            };

            FixerApiProvider provider = new FixerApiProvider(config);
            //Act
            //Assert
            Assert.ThrowsAsync<InvalidOperationException>(() => provider.GetCurentCurrencyRate("PLN", "USD"));
        }

        [Fact]
        public void GetCurrencyByDateRange_ThrowsInvalidOperationException_WhenProviderNotInitialized()
        {
            //Arrange
            FixerConfiguration config = new FixerConfiguration()
            {
                BaseUri = "https://api.fixer.io",
                MinimumDateRange = new DateTime(2000, 01, 01),
                SupportedCurrencies = new[] { "USD", "EUR", "PLN" }
            };

            FixerApiProvider provider = new FixerApiProvider(config);
            //Act
            //Assert
            Assert.ThrowsAsync<InvalidOperationException>(() => provider.GetCurrencyByDateRange("PLN", "USD", DateTime.Now.AddDays(-2), DateTime.Now));
        }

        [Fact]
        public void GetCurrencyRateByDate_ThrowsInvalidOperationException_WhenProviderNotInitialized()
        {
            //Arrange
            FixerConfiguration config = new FixerConfiguration()
            {
                BaseUri = "https://api.fixer.io",
                MinimumDateRange = new DateTime(2000, 01, 01),
                SupportedCurrencies = new[] { "USD", "EUR", "PLN" }
            };

            FixerApiProvider provider = new FixerApiProvider(config);
            //Act
            //Assert
            Assert.ThrowsAsync<InvalidOperationException>(() => provider.GetCurrencyRateByDate("PLN", "USD", DateTime.Now.AddDays(-2)));
        }

        [Fact]
        public void GetCurentCurrencyRate_ThrowsInvalidOperationException_WhenWrongCurrencesProvided()
        {
            //Arrange
            FixerConfiguration config = new FixerConfiguration()
            {
                BaseUri = "https://api.fixer.io",
                MinimumDateRange = new DateTime(2000, 01, 01),
                SupportedCurrencies = new[] { "USD", "EUR", "PLN" }
            };

            FixerApiProvider provider = new FixerApiProvider(config);
            //Act
            //Assert
            Assert.ThrowsAsync<InvalidOperationException>(() => provider.GetCurentCurrencyRate("PLN", "GBP"));
        }

        [Fact]
        public void GetCurrencyRateByDate_ThrowsInvalidOperationException_WhenWrongCurrencesProvided()
        {
            //Arrange
            FixerConfiguration config = new FixerConfiguration()
            {
                BaseUri = "https://api.fixer.io",
                MinimumDateRange = new DateTime(2000, 01, 01),
                SupportedCurrencies = new[] { "USD", "EUR", "PLN" }
            };

            FixerApiProvider provider = new FixerApiProvider(config);
            //Act
            //Assert
            Assert.ThrowsAsync<InvalidOperationException>(() => provider.GetCurrencyRateByDate("PLN", "GBP", DateTime.Now));
        }

        [Fact]
        public void GetCurrencyByDateRange_ThrowsInvalidOperationException_WhenWrongCurrencesProvided()
        {
            //Arrange
            FixerConfiguration config = new FixerConfiguration()
            {
                BaseUri = "https://api.fixer.io",
                MinimumDateRange = new DateTime(2000, 01, 01),
                SupportedCurrencies = new[] { "USD", "EUR", "PLN" }
            };

            FixerApiProvider provider = new FixerApiProvider(config);
            //Act
            //Assert
            Assert.ThrowsAsync<InvalidOperationException>(() => provider.GetCurrencyByDateRange("PLN", "GBP", DateTime.Now.AddDays(-2), DateTime.Now));
        }
    }
}
