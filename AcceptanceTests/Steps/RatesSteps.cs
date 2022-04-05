using FluentAssertions;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TestApp.AcceptanceTests.Context;
using TestApp.Services.Interfaces;

namespace TestApp.AcceptanceTests.Steps
{
    [Binding]
    public class RatesSteps
    {
        private readonly RatesContext _ratesContext;
        private readonly IOpenExchangeRatesService _openExchangeRatesService;

        public RatesSteps(RatesContext ratesContext, IOpenExchangeRatesService openExchangeRatesService)
        {
            _ratesContext = ratesContext;
            _openExchangeRatesService = openExchangeRatesService;
        }

        [When(@"the user requests all rates")]
        public async Task WhenTheUserRequestData()
        {
            var rates = await _openExchangeRatesService.GetAllRates();
            _ratesContext.Rates = rates;
        }
        
        [Then(@"a non empty list with rates is displayed")]
        public void ThenANonEmptyListWithRatesIsDisplayed()
        {
            var expectedRatesList = _ratesContext.Rates;
            expectedRatesList.Should().NotBeEmpty();
        }
    }
}
