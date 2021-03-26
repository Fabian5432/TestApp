using FluentAssertions;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TestApp.AcceptanceTests.Context;
using TestApp.Services.Interfaces;

namespace TestApp.AcceptanceTests.Steps
{
    [Binding]
    public class QuatesSteps
    {
        private readonly QuatesContext _quatesContext;
        private readonly ICurrencyLayerService _currencyLayerService;

        public QuatesSteps(QuatesContext quatesContext, ICurrencyLayerService currencyLayerService)
        {
            _quatesContext = quatesContext;
            _currencyLayerService = currencyLayerService;

        }

        [Given(@"a user is using the CurrencyLayer api")]
        public void GivenAUserIsUsingThe()
        {
            // empty step
        }
        
        [When(@"the user requests all quates")]
        public async Task WhenTheUserTypeList()
        {
            var quates = await _currencyLayerService.GetAllQuotes();

            _quatesContext.Quotes = quates;
        }
        
        [Then(@"a non empty list with quates is displayed")]
        public void ThenANonEmptyListIsDisplayed()
        {
            var expectedQuatesList = _quatesContext.Quotes;
            expectedQuatesList.Should().NotBeEmpty();
        }
    }
}
