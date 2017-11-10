using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CurrencyTest.Infrastructure.Helpers;
using CurrencyTest.Infrastructure.Interfaces;
using CurrencyTest.Infrastructure.Models;
using CurrencyTest.Services;
using Moq;
using Xunit;

namespace CurrencyTest.Tests.Services
{
    public class CurrenciesServiceTests
    {
        [Fact]
        public async void GetCurrentConcurrencyRate_CallsApiOnce_WhenFired()
        {
            //Arrange
            string firstCurrency = "USD";
            string secondCurrency = "PLN";
            Mock<ICurrencyApi> apiMock = new Mock<ICurrencyApi>();
            Mock<ICurrenciesConfiguration> config = new Mock<ICurrenciesConfiguration>();
            config.SetupGet(x => x.AvaibleCurrencies).Returns(new[] { "USD", "PLN" });
            CurrenciesService service = new CurrenciesService(apiMock.Object, config.Object);

            //Act
            await service.GetCurrentCurrencyRate(firstCurrency, secondCurrency);

            //Assert
            apiMock.Verify(x => x.GetCurentCurrencyRate(firstCurrency, secondCurrency), Times.Once());
        }

        [Fact]
        public async void GetAverageCurencyRateForDates_CallsApiOnce_WhenFiredWithDateRange()
        {
            //Arrange
            DateTime startDate = DateTime.Now.AddDays(-5);
            DateTime endDate = DateTime.Now;
            int apiFireTimes = DateTimeHelper.GetDatesCollection(startDate, endDate).Count();
            string firstCurrency = "USD";
            string secondCurrency = "PLN";
            var apiReturnValue = new List<CurrencyPair>()
            {
                new CurrencyPair(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<float>(), It.IsAny<DateTime>(),
                    It.IsAny<DateTime>()),
                new CurrencyPair(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<float>(), It.IsAny<DateTime>(),
                    It.IsAny<DateTime>())
            };

            Mock<ICurrencyApi> apiMock = new Mock<ICurrencyApi>();
            apiMock
                .Setup(x => x.GetCurrencyByDateRange(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>(),
                    It.IsAny<DateTime>()))
                .ReturnsAsync(() => apiReturnValue);
                
            Mock<ICurrenciesConfiguration> config = new Mock<ICurrenciesConfiguration>();
            config.SetupGet(x => x.AvaibleCurrencies).Returns(new[] { "USD", "PLN" });
            CurrenciesService service = new CurrenciesService(apiMock.Object, config.Object);

            //Act
            await service.GetAverageCurencyRateForDates(firstCurrency, secondCurrency, startDate, endDate);

            //Assert
            apiMock.Verify(x => x.GetCurrencyByDateRange(firstCurrency, secondCurrency, startDate, endDate), Times.Once());
        }

        [Fact]
        public async void GetAverageCurencyRateForDates_ReturnsAverageValue_FromApiValues()
        {
            //Arrange
            float firstValue = 10;
            float secondValue = 20;
            DateTime startDate = DateTime.Now.AddDays(-5);
            DateTime endDate = DateTime.Now;
            int apiFireTimes = DateTimeHelper.GetDatesCollection(startDate, endDate).Count();
            string firstCurrency = "USD";
            string secondCurrency = "PLN";
            var apiReturnValue = new List<CurrencyPair>()
            {
                new CurrencyPair(It.IsAny<string>(), It.IsAny<string>(), firstValue, It.IsAny<DateTime>(),
                    It.IsAny<DateTime>()),
                new CurrencyPair(It.IsAny<string>(), It.IsAny<string>(), secondValue, It.IsAny<DateTime>(),
                    It.IsAny<DateTime>())
            };

            Mock<ICurrencyApi> apiMock = new Mock<ICurrencyApi>();
            apiMock
                .Setup(x => x.GetCurrencyByDateRange(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>(),
                    It.IsAny<DateTime>()))
                .ReturnsAsync(() => apiReturnValue);

            Mock<ICurrenciesConfiguration> config = new Mock<ICurrenciesConfiguration>();
            config.SetupGet(x => x.AvaibleCurrencies).Returns(new[] { "USD", "PLN" });
            CurrenciesService service = new CurrenciesService(apiMock.Object, config.Object);

            //Act
            var result = await service.GetAverageCurencyRateForDates(firstCurrency, secondCurrency, startDate, endDate);

            //Assert
            Assert.Equal(result.ExchangeRate, (firstValue + secondValue) /2);
        }

        [Fact]
        public async void GetMaximumCurencyRateForDates_ReturnsAverageValue_FromApiValues()
        {
            //Arrange
            float firstValue = 10;
            float secondValue = 20;
            DateTime startDate = DateTime.Now.AddDays(-5);
            DateTime endDate = DateTime.Now;
            int apiFireTimes = DateTimeHelper.GetDatesCollection(startDate, endDate).Count();
            string firstCurrency = "USD";
            string secondCurrency = "PLN";
            var apiReturnValue = new List<CurrencyPair>()
            {
                new CurrencyPair(It.IsAny<string>(), It.IsAny<string>(), firstValue, It.IsAny<DateTime>(),
                    It.IsAny<DateTime>()),
                new CurrencyPair(It.IsAny<string>(), It.IsAny<string>(), secondValue, It.IsAny<DateTime>(),
                    It.IsAny<DateTime>())
            };

            Mock<ICurrencyApi> apiMock = new Mock<ICurrencyApi>();
            apiMock
                .Setup(x => x.GetCurrencyByDateRange(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>(),
                    It.IsAny<DateTime>()))
                .ReturnsAsync(() => apiReturnValue);

            Mock<ICurrenciesConfiguration> config = new Mock<ICurrenciesConfiguration>();
            config.SetupGet(x => x.AvaibleCurrencies).Returns(new[] { "USD", "PLN" });
            CurrenciesService service = new CurrenciesService(apiMock.Object, config.Object);

            //Act
            var result = await service.GetMaximumCurencyRateForDates(firstCurrency, secondCurrency, startDate, endDate);

            //Assert
            Assert.Equal(result.ExchangeRate, secondValue);
        }

        [Fact]
        public async void GetMinimumCurencyRateForDates_ReturnsAverageValue_FromApiValues()
        {
            //Arrange
            float firstValue = 10;
            float secondValue = 20;
            DateTime startDate = DateTime.Now.AddDays(-5);
            DateTime endDate = DateTime.Now;
            int apiFireTimes = DateTimeHelper.GetDatesCollection(startDate, endDate).Count();
            string firstCurrency = "USD";
            string secondCurrency = "PLN";
            var apiReturnValue = new List<CurrencyPair>()
            {
                new CurrencyPair(It.IsAny<string>(), It.IsAny<string>(), firstValue, It.IsAny<DateTime>(),
                    It.IsAny<DateTime>()),
                new CurrencyPair(It.IsAny<string>(), It.IsAny<string>(), secondValue, It.IsAny<DateTime>(),
                    It.IsAny<DateTime>())
            };

            Mock<ICurrencyApi> apiMock = new Mock<ICurrencyApi>();
            apiMock
                .Setup(x => x.GetCurrencyByDateRange(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>(),
                    It.IsAny<DateTime>()))
                .ReturnsAsync(() => apiReturnValue);

            Mock<ICurrenciesConfiguration> config = new Mock<ICurrenciesConfiguration>();
            config.SetupGet(x => x.AvaibleCurrencies).Returns(new[] { "USD", "PLN" });
            CurrenciesService service = new CurrenciesService(apiMock.Object, config.Object);

            //Act
            var result = await service.GetMinimumCurencyRateForDates(firstCurrency, secondCurrency, startDate, endDate);

            //Assert
            Assert.Equal(result.ExchangeRate, firstValue);
        }

        [Fact]
        public async void GetCurrencyRateByDate_CallsApi_WhenFired()
        {
            //Arrange
            DateTime date = DateTime.Now;
            string firstCurrency = "USD";
            string secondCurrency = "PLN";
            Mock<ICurrencyApi> apiMock = new Mock<ICurrencyApi>();
            Mock<ICurrenciesConfiguration> config = new Mock<ICurrenciesConfiguration>();
            config.SetupGet(x => x.AvaibleCurrencies).Returns(new[] { "USD", "PLN" });
            CurrenciesService service = new CurrenciesService(apiMock.Object, config.Object);

            //Act
            await service.GetCurrencyRateByDate(firstCurrency, secondCurrency, date);

            //Assert
            apiMock.Verify(x => x.GetCurrencyRateByDate(firstCurrency, secondCurrency, date), Times.Once());
        }

        [Fact]
        public async void GetCurrentConcurrencyRate_ThrowsException_WhenCurrencyIsNotConfigured()
        {
            //Arrange
            string firstCurrency = "USD";
            string secondCurrency = "PLN";
            Mock<ICurrencyApi> apiMock = new Mock<ICurrencyApi>();
            Mock<ICurrenciesConfiguration> config = new Mock<ICurrenciesConfiguration>();
            config.SetupGet(x => x.AvaibleCurrencies).Returns(new[] { "EUR", "PLN" });
            CurrenciesService service = new CurrenciesService(apiMock.Object, config.Object);

            //Act
            //Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => service.GetCurrentCurrencyRate(firstCurrency, secondCurrency));
        }
    }
}
