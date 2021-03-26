using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Idioms;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.Models;
using TestApp.Services.Interfaces;
using TestApp.ViewModels;

namespace TestApp.UnitTests.ViewModels
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
            var randomRates = _fixture.CreateMany<RatesModel>(10);
            _openExchangeRatesServiceMock.Setup(x => x.GetAllRates())
                                         .ReturnsAsync(randomRates.ToList());
            _fixture.Inject(_openExchangeRatesServiceMock);

            var SUT = _fixture.Create<RatesViewModel>();

            // Act 
            await SUT.GetAllRates();

            // Assert 
            SUT.RatesItems.Should().NotBeEmpty();
            SUT.RatesItems.Should().HaveCount(10);
        }

        [Test]
        public async Task ShouldGetAllQuates()
        {
            // Arrange 
            var randomQuotes = _fixture.CreateMany<QuotesModel>(10);
            _currencyLayerServiceMock.Setup(x => x.GetAllQuotes())
                                     .ReturnsAsync(randomQuotes.ToList());

            _fixture.Inject(_currencyLayerServiceMock);

            var SUT = _fixture.Create<RatesViewModel>();

            // Act 
            await SUT.GetAllQuotes();

            // Assert 
            SUT.QuotesItems.Should().NotBeEmpty();
            SUT.QuotesItems.Should().HaveCount(10);
        }

        [TestCase("AED", 0.5)]
        [TestCase("USD", 1)]
        [TestCase("AFN", 0.1)]
        [TestCase("ALN", 0.2)]
        public async Task ShouldGetCurrentQuotesAndRatesValueForGivenCurrency(string expectedCurrency, double expectedCurrencyValue)
        {
            // Arrange 
            var randomQuotes = _fixture.CreateMany<QuotesModel>(3);
            var randomRates = _fixture.CreateMany<RatesModel>(3);
            var expectedQuotes = new List<QuotesModel>(randomQuotes);
            var expectedRates = new List<RatesModel>(randomRates);

            var rate = new RatesModel(expectedCurrency, expectedCurrencyValue);
            var quate = new QuotesModel(expectedCurrency, expectedCurrencyValue);

            _openExchangeRatesServiceMock.Setup(x => x.GetAllRates())
                                         .ReturnsAsync(expectedRates);
            _currencyLayerServiceMock.Setup(x => x.GetAllQuotes())
                                     .ReturnsAsync(expectedQuotes);

            _fixture.Inject(_openExchangeRatesServiceMock);
            _fixture.Inject(_currencyLayerServiceMock);

            expectedQuotes.Add(quate);
            expectedRates.Add(rate);

            var SUT = _fixture.Create<RatesViewModel>();

            // Act 
            await SUT.GetCurrentRateFor(expectedCurrency);

            // Assert 
            SUT.OpenExchangeRatesApiRateValue.Should().NotBe(null);
            SUT.CurrencyLayerServiceApiValue.Should().NotBe(null);
            SUT.OpenExchangeRatesApiRateValue.Should().Be(expectedCurrencyValue);
            SUT.CurrencyLayerServiceApiValue.Should().Be(expectedCurrencyValue);
            _currencyLayerServiceMock.Verify(x => x.GetAllQuotes(), Times.Once);
            _openExchangeRatesServiceMock.Verify(x => x.GetAllRates(), Times.Once);
        }

        [Test]
        public void ShouldThrowExceptionWhenNoElemenAreAvailable()
        {   
            // Arrange
            var SUT = _fixture.Create<RatesViewModel>();

            // Act 
            Func<Task> act1 = async () => { await SUT.GetAllQuotes(); };
            Func<Task> act2 = async () => { await SUT.GetAllRates(); };

            // Assert 
            act1.Should().Throw<Exception>().WithMessage("Error loading quotes from currency layer api");
            act2.Should().Throw<Exception>().WithMessage("Error loading rates from Open exchange api");
        }

        [Test]
        public void ShouldThrowExceptionIfCurrencyNameIsNullOrWhiteSpace()
        {  
            // Arrange 
            var randomQuotes = _fixture.CreateMany<QuotesModel>(3);
            var randomRates = _fixture.CreateMany<RatesModel>(3);
            var expectedQuotes = new List<QuotesModel>(randomQuotes);
            var expectedRates = new List<RatesModel>(randomRates);

            _openExchangeRatesServiceMock.Setup(x => x.GetAllRates())
                                         .ReturnsAsync(expectedRates);
            _currencyLayerServiceMock.Setup(x => x.GetAllQuotes())
                                     .ReturnsAsync(expectedQuotes);

            _fixture.Inject(_openExchangeRatesServiceMock);
            _fixture.Inject(_currencyLayerServiceMock);

            var SUT = _fixture.Create<RatesViewModel>();

            // Act 
            Func<Task> act1 = async () => { await SUT.GetCurrentRateFor(null); };
            Func<Task> act2 = async () => { await SUT.GetBestCurrentRateFor(null); };

            // Assert
            act1.Should().Throw<Exception>().WithMessage("Currency name must be specified");
            act2.Should().Throw<Exception>().WithMessage("Currency name must be specified");
        }

        #endregion
    }
}
