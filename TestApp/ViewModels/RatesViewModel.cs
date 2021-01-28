using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.Services.Interfaces;
using TestApp.Models;
using TestApp.ViewModels.Base;

namespace TestApp.ViewModels
{
    public class RatesViewModel : BaseViewModel
    {
        #region Properties

        public List<RatesModel> RatesItems { get; } = new List<RatesModel>();
        public List<QuotesModel> QuotesItems { get; } = new List<QuotesModel>();

        public double OpenExchangeRatesApiRateValue { get; set; } = default;
        public double CurrencyLayerServiceApiValue { get; set; } = default;
        public double BestCurrencyValue { get; set; } = default;

        #endregion

        #region Fields

        private readonly IOpenExchangeRatesService _openExchangeRatesService;
        private readonly ICurrencyLayerService _currencyLayerService;

        #endregion

        #region Constructor

        public RatesViewModel(IOpenExchangeRatesService openExchangeRatesService,
                              ICurrencyLayerService currencyLayerService)
        {
            _openExchangeRatesService = openExchangeRatesService ??
                throw new ArgumentNullException(nameof(openExchangeRatesService));
            _currencyLayerService = currencyLayerService ??
                throw new ArgumentNullException(nameof(currencyLayerService));
        }

        #endregion

        #region Methods

        public async Task GetAllRates()
        {
            try
            {
                RatesItems.Clear();
                var rates = await _openExchangeRatesService.GetAllRates();
                foreach (var rate in rates )
                {
                    RatesItems.Add(rate);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error loading rates from Open exchange api", ex);
            }
        }

        public async Task GetAllQuotes()
        {
            try
            {
                QuotesItems.Clear();
                var quotes = await _currencyLayerService.GetAllQuotes();
                foreach (var quote in quotes)
                {
                    QuotesItems.Add(quote);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error loading quotes from curreny layer api", ex);
            }
        }

        public async Task GetBestCurrentRateFor(string currencyName)
        {
            if (string.IsNullOrWhiteSpace(currencyName))
            {
                throw new Exception($"Currency name must be specified");
            }

            try
            {
                var allRates = await _openExchangeRatesService.GetAllRates();
                var allQuotes = await _currencyLayerService.GetAllQuotes();

                OpenExchangeRatesApiRateValue = allRates.FirstOrDefault(r => r.RateName.Contains(currencyName)).RateValue;
                CurrencyLayerServiceApiValue = allQuotes.FirstOrDefault(q => q.QuoteName.Contains(currencyName)).QuoteValue;

                BestCurrencyValue = (OpenExchangeRatesApiRateValue > CurrencyLayerServiceApiValue) ? OpenExchangeRatesApiRateValue : CurrencyLayerServiceApiValue;
            }
            catch(Exception ex)
            {
                throw new Exception($"Error loading the best current value with the given currency name: {currencyName}", ex);
            }
        }

        public async Task GetCurrentRateFor(string currencyName)
        {
            if (string.IsNullOrWhiteSpace(currencyName))
            {
                throw new Exception($"Currency name must be specified");
            }

            try
            {   
                var allRates = await _openExchangeRatesService.GetAllRates();
                var allQuotes = await _currencyLayerService.GetAllQuotes();

                OpenExchangeRatesApiRateValue = allRates.FirstOrDefault(r => r.RateName.Contains(currencyName)).RateValue;
                CurrencyLayerServiceApiValue = allQuotes.FirstOrDefault(q => q.QuoteName.Contains(currencyName)).QuoteValue;
            }
            catch(Exception ex)
            {
                throw new Exception($"Error getting current rate value with the given currency name: {currencyName}", ex);
            }
        }

        #endregion

    }
}
