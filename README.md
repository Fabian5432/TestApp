# TestApp
This console application integrates 2 REST api's to query the best exchange rate for different currencies. Default currency is USD. 

- Pattern used MVVM(Model-view-viewModel)
- Refit nuget was used for requesting data from both api's 
- Integration, unit test are done using Nunit framework,AutoFixture, AutoFixture.AutoMoq, AutoFixture.Idioms;

Example of using the OpenExchangeRatesService:
  var openExchangeRatesService =  new OpenExchangeRatesService(app_id: "Your app id");

Example of using the CurrencyLayerService:
  var currencyLayerService =  new CurrencyLayerService(access_key: "Your access_key");
