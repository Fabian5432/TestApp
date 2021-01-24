using System;
using System.Configuration;
using TestApp.Services.Interfaces;

namespace TestApp.Services
{
    public static class ServiceLocator
    {
        #region Fields

        private static IOpenExchangeRatesService _openExchangeRatesService;
        private static ICurrencyLayerService _currencyLayerService;

        #endregion

        #region Properties

        public static IOpenExchangeRatesService GetOpenExchangeRatesService =>
            _openExchangeRatesService ??= new OpenExchangeRatesService(app_id: ConfigurationManager.AppSettings["app_id"]);
        public static ICurrencyLayerService GetCurrencyLayerService =>
            _currencyLayerService ??= new CurrencyLayerService(access_key: ConfigurationManager.AppSettings["access_key"]);

        #endregion
    }
}
