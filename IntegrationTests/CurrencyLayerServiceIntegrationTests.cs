using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestApp.Services;

namespace IntegrationTests
{   
    [TestFixture]
    class CurrencyLayerServiceIntegrationTests
    {
        [Test]
        public async Task ShouldGetLiveValuesWithValidAccessKey()
        {
            // Arrange 
            var SUT = new CurrencyLayerService("980b4fe950b3d8d23a5a6eef697865af");

            // Act 
            var result = await SUT.GetAllQuotes();

            // Assert
            result.Should().NotBeNull();
            result.Should().NotBeEmpty();
        }

        [Test]
        public void ShouldThrowExceptionIfAppIdIsInvalid()
        {
            // Arrange
            var SUT = new OpenExchangeRatesService("dummy");

            // Assert
            Assert.ThrowsAsync<Exception>(async () => await SUT.GetAllRates());
        }
    }
}
