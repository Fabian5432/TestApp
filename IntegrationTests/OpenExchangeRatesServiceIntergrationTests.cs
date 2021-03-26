using FluentAssertions;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using TestApp.Services;

namespace TestApp.IntegrationTests
{   
    [TestFixture]
    public class OpenExchangeRatesServiceIntergrationTests
    {
        [Test]
        public async Task ShouldGetLatestValuesWithValidAppId()
        {  
            // Arrange 
            var SUT = new OpenExchangeRatesService("1de86dfd996b4c9da20c0b3fa6eefaa4");

            // Act 
            var result = await SUT.GetAllRates();

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