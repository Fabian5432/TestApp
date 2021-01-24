using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.Services.Interfaces;
using TestApp.Models;
using TestApp.ViewModels.Base;
using System.Diagnostics;

namespace TestApp.ViewModels
{
    public class RatesViewModel : BaseViewModel
    {
        #region Properties

        public List<RatesModel> RatesItems { get; } = new List<RatesModel>();
        public List<QuotesModel> QuotesItems { get; } = new List<QuotesModel>();

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
                Debug.WriteLine(ex);
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
                Debug.WriteLine(ex);
            }
        }

        public async Task<RatesModel> GetCurrentRateFor(string currencyName)
        {   
            var allRates = await _openExchangeRatesService.GetAllRates();

            return allRates.FirstOrDefault(r => r.RateName.Contains(currencyName));
        }

        public async Task<QuotesModel> GetCurrentQuoteFor(string currencyName)
        {
             var allQuotes = await _currencyLayerService.GetAllQuotes();

             return allQuotes.FirstOrDefault(q => q.QuoteName.Contains(currencyName));
        }

        #endregion

    }
}
