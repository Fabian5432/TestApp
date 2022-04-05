using BoDi;
using System;
using TechTalk.SpecFlow;
using TestApp.Services;
using TestApp.Services.Interfaces;

namespace TestApp.AcceptanceTests.Binding
{   
    [Binding]
    class ContainerSetupBinding
    {
        #region Fields

        private readonly IObjectContainer _objectContainer;

        #endregion

        #region Constructor

        public ContainerSetupBinding(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer ?? 
                throw new ArgumentNullException(nameof(objectContainer));
        }

        #endregion

        #region SetupBeforeScenarios

        [Given(@"a user is using the (.*) api")]
        public void GivenAUserIsUsingThe(string api)
        {
            switch(api)
            {
                case "OpenExchangeRates":
                    IOpenExchangeRatesService openExchangeRatesService = new OpenExchangeRatesService("1de86dfd996b4c9da20c0b3fa6eefaa4");
                    _objectContainer.RegisterInstanceAs(openExchangeRatesService);
                    break;
                case "CurrencyLayer":
                    ICurrencyLayerService currencyLayerService = new CurrencyLayerService("980b4fe950b3d8d23a5a6eef697865af");
                    _objectContainer.RegisterInstanceAs(currencyLayerService);
                    break;
            }
        }

        #endregion
    }
}
