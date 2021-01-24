using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Idioms;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.Models;
using TestApp.Services.Interfaces;
using TestApp.ViewModels;

namespace Tests.ViewModels
{  
    [TestFixture]
    [TestOf(nameof(RatesViewModel))]
    public class RatesViewModelTests
    {
		#region Fields

		private IFixture _fixture;
        private Mock<IOpenExchangeRatesService> _openExchangeRatesServiceMock;
        private Mock<ICurrencyLayerService> _currencyLayerServiceMock;

		#endregion

		#region Setup Methods

		[SetUp]
		public void SetUp()
		{
			_fixture = new Fixture();
			_fixture.Customize(new AutoMoqCustomization());

            _openExchangeRatesServiceMock = new Mock<IOpenExchangeRatesService>();
            _currencyLayerServiceMock = new Mock<ICurrencyLayerService>();
		}

        #endregion

        #region Test Methods

        [Test]
        public void ShouldThrowGuardClauseExceptionForNullConstructorParameters()
        {
            // Arrange
            var guardClauseAssertion = new GuardClauseAssertion(_fixture);

            // Act / Assert
            guardClauseAssertion.Verify(typeof(RatesViewModel).GetConstructors());
        }

        [Test]
        public async Task ShouldGetAllRates()
        {
            // Arrange 
            var random_rates = _fixture.CreateMany<RatesModel>(1);
            _openExchangeRatesServiceMock.Setup(x => x.GetAllRates())
                                         .ReturnsAsync(random_rates.ToList());
            _fixture.Inject(_openExchangeRatesServiceMock);

            var SUT = _fixture.Create<RatesViewModel>();

            // Act 
            await SUT.GetAllRates();

            // Assert 
            SUT.RatesItems.Should().NotBeEmpty();
        }

        [Test]
        public async Task ShouldGetAllQuates()
        {
            // Arrange 
            var random_quotes = _fixture.CreateMany<QuotesModel>(1);
            _currencyLayerServiceMock.Setup(x => x.GetAllQuotes())
                                     .ReturnsAsync(random_quotes.ToList());
            _fixture.Inject(_currencyLayerServiceMock);

            var SUT = _fixture.Create<RatesViewModel>();

            // Act 
            await SUT.GetAllQuotes();

            // Assert 
            SUT.QuotesItems.Should().NotBeEmpty();
        }

        [TestCase("AED", 0.5)]
        [TestCase("USD", 1)]
        [TestCase("AFN", 0.1)]
        [TestCase("ALN", 0.2)]
        public async Task ShouldGetCurrentQuotes(string expected_currency, double quoteValue)
        {
            // Arrange 
            var random_quotes = _fixture.CreateMany<QuotesModel>(3);
            var expected_quotes = new List<QuotesModel>(random_quotes);

            var quate = new QuotesModel(expected_currency, quoteValue);

            _currencyLayerServiceMock.Setup(x => x.GetAllQuotes())
                                     .ReturnsAsync(expected_quotes);

            _fixture.Inject(_currencyLayerServiceMock);

            expected_quotes.Add(quate);

            var SUT = _fixture.Create<RatesViewModel>();

            // Act 
            var data = await SUT.GetCurrentQuoteFor(expected_currency);

            // Assert 
            data.Should().NotBeNull();
            data.QuoteName.Should().Be(expected_currency);
            _currencyLayerServiceMock.Verify(x => x.GetAllQuotes(), Times.Once);
        }

        [TestCase("AED", 0.5)]
        [TestCase("USD", 1)]
        [TestCase("AFN", 0.1)]
        [TestCase("ALN", 0.2)]
        public async Task ShouldGetCurrentRates(string expected_currency, double rateValue)
        {
            // Arrange 
            var random_rates = _fixture.CreateMany<RatesModel>(3);
            var expected_rates = new List<RatesModel>(random_rates);

            var rate = new RatesModel(expected_currency, 1.5);

            _openExchangeRatesServiceMock.Setup(x => x.GetAllRates())
                                         .ReturnsAsync(expected_rates);

            _fixture.Inject(_openExchangeRatesServiceMock);

            expected_rates.Add(rate);

            var SUT = _fixture.Create<RatesViewModel>();

            // Act 
            var data = await SUT.GetCurrentRateFor(expected_currency);

            // Assert 
            data.Should().NotBeNull();
            data.RateName.Should().Be(expected_currency);
            _openExchangeRatesServiceMock.Verify(x => x.GetAllRates(), Times.Once);
        }

        #endregion
    }
}
